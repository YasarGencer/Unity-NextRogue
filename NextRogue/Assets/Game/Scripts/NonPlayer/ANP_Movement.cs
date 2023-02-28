using System;
using System.Collections;
using UnityEngine;
using UniRx;
using static EventManager;
using UnityEngine.UIElements;

public abstract class ANP_Movement : MonoBehaviour
{
    protected NP_MainController _mainController;
    protected IDisposable _updateRX;
    protected Vector2 _patrolPosition;
    public virtual void Initialize(NP_MainController mainController) {
        _mainController = mainController;
        UnFreeze();
        RegisterEvents();
        StartCoroutine(PatrolPos());
    }

    protected abstract void UpdateRX(long obj);
    IEnumerator PatrolPos() {
        _patrolPosition = new Vector2(transform.position.x, transform.position.y) + Direction2D.GetRandomEightDirection();
        yield return new WaitForSeconds(1f);
        StartCoroutine(PatrolPos());
    }

    public virtual IEnumerator FreezeMovement(float time) {
        Freeze();
        yield return new WaitForSeconds(time);
        UnFreeze();
    }
    public virtual IEnumerator SlowSpeed(float time, float percentage) {
        Slow(percentage);
        yield return new WaitForSeconds(time);
        UnSlow();
    }

    public virtual void Freeze() {
        _updateRX?.Dispose();
    }
    protected virtual void UnFreeze() {
        if (_gamePaused)
            return;
        _updateRX?.Dispose();
        _updateRX = Observable.EveryUpdate().TakeUntilDisable(this).Subscribe(UpdateRX);
    }
    protected virtual void Slow(float percentage) {
        _mainController.Stats.SpeelHolder = _mainController.Stats.Speed * (1 - percentage);
    }
    protected virtual void UnSlow() {
        _mainController.Stats.SpeelHolder = _mainController.Stats.Speed;
    }
    protected void Rotate(float posX) {
        if (posX > transform.position.x + 0.1f)
            transform.localScale = new Vector3(1, 1, 1);
        else if (posX < transform.position.x - 0.1f)
            transform.localScale = new Vector3(-1, 1, 1);
    }
    public virtual void Die() {
        _updateRX?.Dispose();
    }
    // EVENTS
    bool _gamePaused = false;
    void RegisterEvents() { 
        MainManager.Instance.EventManager.onGamePause += OnGamePause;
        MainManager.Instance.EventManager.onGameUnPause += OnGameUnPause;
    }
    void OnGamePause() {
        Freeze();
        _gamePaused = true;
    }
    void OnGameUnPause() {
        _gamePaused = false;
        UnFreeze();
    }
}
