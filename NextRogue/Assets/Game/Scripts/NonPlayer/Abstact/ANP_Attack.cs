using System;
using UnityEngine;
using UniRx;

public abstract class ANP_Attack : MonoBehaviour
{
    protected NP_MainController _mainController;
    protected IDisposable _updateRX;
    protected float _attackTime = 0;
    protected bool _isAttacking = false;
    public virtual void Initialize(NP_MainController mainController) {
        _mainController = mainController;
        SetAttackTrue();
        RegisterEvents(); 
        _isAttacking = false;
    }
    protected virtual void UpdateRX(long obj) {
        if (_mainController.Target.Target == null)
            return;
        if (_isAttacking == false)
            if (_attackTime <= 0)
                if (_mainController.Distance(_mainController.Target.Target.transform) < _mainController.Stats.AttackRange)
                    if (ShootRay())
                        Attack();

        AttackLimiter();
    }
    protected abstract void Attack();
    protected virtual void AttackLimiter() {
        if (_isAttacking)
            return;
        _attackTime -= Time.deltaTime * Time.timeScale;
    }
    public virtual void SetAttackFalse() {
        _updateRX?.Dispose();
    }
    protected virtual void SetAttackTrue() {
        if (MainManager.Instance.GameManager.GamePaused)
            return;
        _updateRX?.Dispose();
        _updateRX = Observable.EveryUpdate().TakeUntilDisable(this).Subscribe(UpdateRX);
    }
    public virtual void Die() {
        UnRegisterEvents();
        _updateRX?.Dispose(); 
    }
    protected bool ShootRay() {
        var direction = GetDir();
        RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, direction, _mainController.Stats.AttackRange);
        foreach (var item in hit)
            if (item.collider != null)
                if (item.collider.gameObject.CompareTag("Player"))
                        return true;
                else if (item.collider.gameObject.CompareTag("Obstacle"))
                    return false;
        return true;
    }
    protected Vector2 GetDir() {
        var targetPos = _mainController.Target.Target.transform.position;
        var direction = targetPos - transform.position;
        return direction;
    }
    // EVENTS 
    void RegisterEvents() {
        MainManager.Instance.EventManager.onGamePause += OnGamePause;
        MainManager.Instance.EventManager.onGameUnPause += OnGameUnPause;
    }
    void UnRegisterEvents() {
        MainManager.Instance.EventManager.onGamePause -= OnGamePause;
        MainManager.Instance.EventManager.onGameUnPause -= OnGameUnPause;
    }
    protected virtual void OnGamePause() {
        SetAttackFalse(); 
    }
    protected virtual void OnGameUnPause() {
        SetAttackTrue(); 
    }
}
