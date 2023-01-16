using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using UniRx.Triggers;

public abstract class AStates : ScriptableObject {

    protected NonPlayerMainController _mainController;
    protected bool isActive = false;

    IDisposable updateRX;
    public virtual void ActivateState(NonPlayerMainController mainController) {
        _mainController= mainController;
        isActive = true;
        updateRX = Observable.EveryUpdate().TakeUntilDisable(_mainController).Subscribe(UpdateRX);
    }
    public virtual void DeactivateState() {
        _mainController = null;
        isActive = false;
        updateRX.Dispose();
    }
    public virtual void UpdateRX(long obj) {
      
    }
}
