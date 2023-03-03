using UnityEngine;
using UniRx;
using System;
using static EventManager;

public class P_Movement : MonoBehaviour
{
    P_MainController _mainController;
    Animator _animator;

    IDisposable _moveRX;
    public Animator Animator { get { return _animator; } }
    private Vector2 _direction;
    public Vector2 Direction { get { return _direction; } }

    public void Initialize(P_MainController mainController) {
        MainManager.Instance.EventManager.onGamePause += OnGamePause;
        MainManager.Instance.EventManager.onGameUnPause += OnGameUnPause;
        _mainController = mainController;
        _animator = _mainController.Animator;
    }
    public void MoveRX(long l) { 
        transform.Translate(_direction * _mainController.Stats.Speed * Time.deltaTime);
    }
    public void SetDirection(Vector2 direction) {
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
    void OnGamePause() {
        _moveRX?.Dispose();
    }
    void OnGameUnPause() {
        _moveRX?.Dispose();
        _moveRX = Observable.EveryUpdate().TakeUntilDisable(this).Subscribe(MoveRX);
    }
}
