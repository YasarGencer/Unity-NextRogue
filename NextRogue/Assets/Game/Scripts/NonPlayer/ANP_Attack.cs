using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public abstract class ANP_Attack : MonoBehaviour
{
    protected NP_MainController _mainController;
    protected IDisposable _updateRX;
    public virtual void Initialize(NP_MainController mainController) {
        _mainController = mainController;
        SetAttackTrue();
        RegisterEvents();
    }
    protected abstract void UpdateRX(long obj);
    public virtual IEnumerator AttackLimiter() {
        SetAttackFalse();
        yield return new WaitForSeconds(_mainController.Stats.AttackSpeed);
        SetAttackTrue();
    }
    public virtual void SetAttackFalse() {
        _updateRX?.Dispose();
    }
    protected virtual void SetAttackTrue() {
        if (InGameManager.Instance.GamePaused)
            return;
        _updateRX?.Dispose();
        _updateRX = Observable.EveryUpdate().TakeUntilDisable(this).Subscribe(UpdateRX);
    }
    public virtual void Die() {
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
    protected virtual void OnGamePause() {
        SetAttackFalse(); 
    }
    protected virtual void OnGameUnPause() { 
        SetAttackTrue(); 
    }
}
