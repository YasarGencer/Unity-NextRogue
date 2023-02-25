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
        _updateRX?.Dispose();
        _updateRX = Observable.EveryUpdate().Subscribe(UpdateRX);
    }
}
