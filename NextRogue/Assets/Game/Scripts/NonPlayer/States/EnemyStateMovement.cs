using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Unity.VisualScripting;
using UnityEditorInternal;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.RuleTile.TilingRuleOutput;
using System.Runtime.Serialization.Formatters;

[CreateAssetMenu(fileName = "EnemyMovementState", menuName = "ScriptableObjects/EnemyStates/EnemyMovementState")]
public class EnemyStateMovement : AStates
{
    
    public override void ActivateState(NonPlayerMainController mainController) {
        base.ActivateState(mainController);
    }
    public override void DeactivateState() {
        base.DeactivateState();
        _mainController.ChangeTarget(null);
    }
    public override void UpdateRX(long obj) {
        Follow();
    }
    public void Follow() {
        Vector2 target = new();
        target = _mainController.AttackTarget != null ? _mainController.AttackTarget.transform.position : _mainController.PatrolTarget;
        if (_mainController.CheckDistance(target) < _mainController.Stats.Range - .5f)
            return;
        _mainController.gameObject.transform.position = Vector3.MoveTowards(_mainController.gameObject.transform.position, target, _mainController.Stats.Speed * Time.deltaTime);
        float dist = _mainController.gameObject.transform.position.x - target.x;
        int scale = 1;
        if (dist > 0f)
            scale = -1;
        else if (dist < -.1f)
            scale = 1;
        _mainController.gameObject.transform.localScale = new Vector3(scale, 1, 1);
    }
   
}