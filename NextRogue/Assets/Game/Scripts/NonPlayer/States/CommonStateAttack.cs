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
        //UNABLE TO ATTACK
        if (!_mainController.CanAttack || !_mainController.Target)
            return;
        //CHECK FOR ATTACK RANGE
        if (CheckRange())
            _mainController.Attack();
    }
    bool CheckRange() {
        //SUMMONS DONT ATTACK PLAYER
        if (_mainController.gameObject.CompareTag("Summoned") && _mainController.Target == _mainController.Player.gameObject)
            return false;
        return _mainController.Stats.Range >= Vector2.Distance(_mainController.gameObject.transform.position, _mainController.Target.transform.position);
    }
}
