using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

public class PlayerMovement : MonoBehaviour
{
    private bool _isInit = false;
    private int _availableDashes;

    InputManager _inputManager;
    Rigidbody2D _rb;
    Animator _animator;

    IDisposable _moveRX;
    public Animator Animator { get { return _animator; } }
    private Vector2 _direction;

    public void Initialize(InputManager inputManager) {
        _inputManager = inputManager;

        _rb = _inputManager.Rb;
        _animator = _inputManager.Animator;
                _availableDashes = _inputManager.PlayerStats.MaxDashes;

        _isInit = true;
    }
    public void MoveRX(long l) {
        transform.Translate(_direction * _inputManager.PlayerStats.Speed * Time.deltaTime);
    }
    public void Direction(Vector2 direction) {
        _direction = direction;

        //set sprite
        _animator.SetFloat("moveDir", _direction.magnitude);


        if (_direction.x > 0)
            this.transform.localScale = new Vector2(1, 1);
        else if (_direction.x < 0)
            this.transform.localScale = new Vector2(-1, 1);



        //disposable movement
        _moveRX?.Dispose();
        if (_direction != Vector2.zero)
            _moveRX = Observable.EveryUpdate().TakeUntilDisable(this).Subscribe(MoveRX);
    }
    public void Dash() {
        if (_availableDashes <= 0)
            return;

        //dash
        _availableDashes--;
        _rb.velocity = _direction * _inputManager.PlayerStats.DashForce;

        //visual effects and cooldowns
        Invoke("StopDash", _inputManager.PlayerStats.DashTime);
        StartCoroutine(DashEffect());
        StartCoroutine(DashTimer());
    }
    public void StopDash() => _rb.velocity = Vector2.zero;
    IEnumerator DashEffect() {
        //variables
        GameObject dashParticle = new GameObject();
        SpriteRenderer sprite;
        Color color;
        
        //effects
        dashParticle.name = "dashParticle";
        dashParticle.transform.position = transform.GetChild(0).position;
        dashParticle.transform.localScale = this.transform.localScale;
        dashParticle.AddComponent<SpriteRenderer>();

        sprite = dashParticle.GetComponent<SpriteRenderer>();
        sprite.sprite = GetComponentInChildren<SpriteRenderer>().sprite;

        color = sprite.color;
        color.a = .25f;
        sprite.color = color;

        Destroy(dashParticle, .25f);

        yield return new WaitForSeconds(_inputManager.PlayerStats.DashTime / 7);
        //check if dash should go on
        if (_rb.velocity != Vector2.zero)
            StartCoroutine(DashEffect());
    }
    IEnumerator DashTimer() {
        if (_availableDashes >= _inputManager.PlayerStats.MaxDashes)
            yield return null;
        yield return new WaitForSeconds(_inputManager.PlayerStats.DashCooldown);
        AddDash();
    }
    void AddDash() {
        _availableDashes++;
    }
}
