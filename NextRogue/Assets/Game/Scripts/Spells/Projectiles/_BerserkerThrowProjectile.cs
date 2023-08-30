using System;
using UnityEngine;
using UniRx;

public class _BerserkerThrowProjectile : AP_Projectile {
    [SerializeField]
    AudioClip _pickUpClip;
    IDisposable _stopRX;
    float _timer = 0;
    bool _isStop;
    [HideInInspector]
    public _BerserkerThrow BerserkThrow;
    [SerializeField]
    GameObject _particle;
    public override void Initialize(Vector3 mousePos, float damage, float time, float speed, DOTInfo dotInfo) {
        base.Initialize(mousePos, damage, time * 2, speed, dotInfo);
        _timer = 0;
        _isStop = false;
        _stopRX = Observable.EveryUpdate().TakeUntilDisable(this).Subscribe(StopRX);
        transform.GetChild(0).GetComponent<Rigidbody2D>().AddTorque(-700f);
    }
    protected override void Move(float speed) {
        base.Move(speed);
    }
    void StopRX(long obj) {
        if (_timer >= 1)
            Stop();
        _timer += Time.deltaTime;
        transform.GetChild(0).localPosition = Vector3.zero;
    }
    void Stop() {
        _stopRX?.Dispose();
        _isStop= true;
        PlaySound();
        GetComponent<CircleCollider2D>().isTrigger = false;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        Destroy(GetComponent<Damager>());
        gameObject.layer = 0;
        transform.GetChild(0).GetComponent<Rigidbody2D>().freezeRotation = true;
        Destroy(Instantiate(_particle, transform.position, Quaternion.identity), 5f);

    }
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.transform.CompareTag("Player")){
            BerserkThrow.RetrieveCooldown();
            AudioManager.PlaySound(_pickUpClip);
            Destroy(gameObject);
        }
    } 
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.layer == 15)
            Stop();
    }
    protected override void OnGamePause() {
        base.OnGamePause();
        _stopRX?.Dispose();
    }
    protected override void OnGameUnPause() {
        base.OnGameUnPause();
        if (!_isStop)
            _stopRX = Observable.EveryUpdate().TakeUntilDisable(this).Subscribe(StopRX);
    }
}
