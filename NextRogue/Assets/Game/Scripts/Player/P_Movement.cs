using UnityEngine;
using UniRx;
using System;

public class P_Movement : MonoBehaviour
{
    P_MainController _mainController;
    Rigidbody2D _rb;
    Animator _animator;

    IDisposable _moveRX;
    float _activeTimer;
    bool _canDash;
    public Animator Animator { get { return _animator; } }
    private Vector2 _direction;
    public Vector2 Direction { get { return _direction; } }

    public void Initialize(P_MainController mainController) {
        _mainController= mainController;

        _rb = _mainController.Rb;
        _animator = _mainController.Animator;
        _canDash = true;
    }
    public void MoveRX(long l) {
        transform.Translate(_direction * _mainController.Stats.Speed * Time.deltaTime);
    }
    public void GetDirection(Vector2 direction) {
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
}
