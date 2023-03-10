using System;
using System.Collections;
using UniRx;
using UnityEngine; 

public class AProjectile : MonoBehaviour
{
    IDisposable _destroyRX;
    protected Vector2 currentVel;
    protected Rigidbody2D _rb;

    protected bool _isInit = false;
    protected float _damage;
    protected float _time;
    protected float _currentTime;
    protected float _speed;

    bool _secondChance = false; 
    private void Awake() {
        RegisterEvents(); 
    }
    public virtual void Initialize(Vector3 targetPos, float damage, float time, float speed) {
        StartCoroutine(Init(targetPos, damage, time, speed));
    }
    protected IEnumerator Init(Vector3 targetPos, float damage, float time, float speed) {
        if (!_isInit && !MainManager.Instance.GameManager.GamePaused)
            _isInit = true;

        _rb = GetComponent<Rigidbody2D>() as Rigidbody2D;
        _speed = speed;
        _time = time >= 1 ? time/2 : 1;
        _damage = damage;
        _currentTime = _time;

        _destroyRX?.Dispose();
        _destroyRX = Observable.EveryUpdate().TakeUntilDisable(this).Subscribe(DestroyRX);

        if (_damage > 0) {
            Damager damager = GetComponent<Damager>() as Damager;
            if (damager)
                GetComponent<Damager>().Initialize(_damage);
        }
        if (targetPos != Vector3.zero)
            SetRotation(targetPos);
        if (_speed >= 0 && _rb != null)
            Move(_speed);

        yield return new WaitForEndOfFrame();
        if(!_isInit)
            StartCoroutine(Init(targetPos, damage, time, speed)); 
    }
    void SetRotation(Vector3 targetPos) {
        targetPos.z = 0f;

        targetPos.x -= transform.position.x;
        targetPos.y -= transform.position.y;

        float angle = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg;
        transform.localRotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    void DestroyRX(long obj) {
        if (_currentTime <= 0) {
            Destroy(gameObject);
            return;
        }
        _currentTime -= Time.deltaTime;
    }
    protected virtual void Move(float speed) {
        _rb.AddForce(transform.right * _rb.mass * speed);
    }  
    // EVENTS
    void RegisterEvents() {
        MainManager.Instance.EventManager.onGamePause += OnGamePause;
        MainManager.Instance.EventManager.onGameUnPause += OnGameUnPause;
    }
    protected virtual void OnGamePause() {
        //if its moving
        if (_rb) {
            currentVel = _rb.velocity;
            _rb.velocity = Vector2.zero;
            //if its supposed to move but hasnt initialized yet
            if (currentVel == Vector2.zero)
                _secondChance = true;
        } 

        _destroyRX?.Dispose(); 
    }
    protected virtual void OnGameUnPause() {
        //if its moving
        if (_rb)  
            _rb.velocity = currentVel;
        //if its supposed to move but hasnt initialized yet
        if (_secondChance) {
            _secondChance = false;
            Move(_speed);
        }

        _destroyRX?.Dispose();
        _destroyRX = Observable.EveryUpdate().TakeUntilDisable(this).Subscribe(DestroyRX); 
    }
}
