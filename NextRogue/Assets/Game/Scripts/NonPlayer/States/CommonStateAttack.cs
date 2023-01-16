using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

[CreateAssetMenu(fileName = "CommonAttackState", menuName = "ScriptableObjects/CommonStates/CommonAttackState")]
public class CommonStateAttack : AStates
{
    public override void ActivateState(NonPlayerMainController mainController) {
        base.ActivateState(mainController);
    }
    public override void DeactivateState() {
        base.DeactivateState();
    }
    public override void UpdateRX(long obj) {
        if (!_mainController.CanAttack || !_mainController.Target)
            return;
        if (CheckRange())
            _mainController.Attack();
    }
    bool CheckRange() {
        return _mainController.Stats.Range >= Vector2.Distance(_mainController.gameObject.transform.position, _mainController.Target.transform.position);
    }
}
