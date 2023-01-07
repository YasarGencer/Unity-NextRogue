using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AEnemyStates {
    protected EnemyStateHandler _stateHandler;
    protected EnemyStateHandler.Enemy _enemy;
    protected GameObject _this;
    public virtual void ActivateState(EnemyStateHandler stateHandler, GameObject gameObject) {
        _stateHandler = stateHandler;
        _enemy = _stateHandler.GetEnemy();
        _this = gameObject;
    }
    public virtual void DeactivateState() {
        _stateHandler = null;
    }
}
