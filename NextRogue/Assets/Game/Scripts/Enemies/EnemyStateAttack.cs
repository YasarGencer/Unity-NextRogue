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

    public override void ActivateState(EnemyMainController mainController) {
        base.ActivateState(mainController);
        _target = GameObject.FindGameObjectWithTag("Player").transform;

        StartFollow();
    }
    public override void DeactivateState() {
        base.DeactivateState();
        _target = null;
    }
    public void Follow(long obj) {
        transform.position = Vector3.MoveTowards(transform.position, _target.position, _mainController.Stats.Speed * Time.deltaTime);
        float dist = transform.position.x - _target.position.x;
        int scale = 1;
        if (dist > 2)
            scale = -1;
        else if (dist < -2)
            scale = 1;
        transform.localScale = new Vector3(scale, 1, 1);
    }
    public void StartFollow() {
        followRX = Observable.EveryUpdate().TakeUntilDisable(this).Subscribe(Follow);
    }
    public void StopFollow() {
        followRX?.Dispose();
    }
}
