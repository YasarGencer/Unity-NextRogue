using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Unity.VisualScripting;
using UnityEditorInternal;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class EnemyStateAttack : AEnemyStates
{
    UnityEngine.Transform _target;
    IDisposable followRX;

    public override void ActivateState(EnemyStateHandler stateHandler, GameObject gameObject) {
        base.ActivateState(stateHandler, gameObject);
        _target = GameObject.FindGameObjectWithTag("Player").transform;

        StartFollow();
    }
    public override void DeactivateState() {
        base.DeactivateState();
        _target = null;
    }
    public void Follow(long obj) {
        _this.transform.position = Vector3.MoveTowards(_this.transform.position, _target.position, _enemy.Speed * Time.deltaTime);
        float dist = _this.transform.position.x - _target.position.x;
        int scale = 1;
        if (dist > 2)
            scale = -1;
        else if (dist < -2)
            scale = 1;
        _this.transform.localScale = new Vector3(scale, 1, 1);
    }
    public void StartFollow() {
        followRX = Observable.EveryUpdate().TakeUntilDisable(_stateHandler).Subscribe(Follow);
    }
    public void StopFollow() {
        followRX?.Dispose();
    }
}
