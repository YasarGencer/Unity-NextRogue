using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

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
        if (!_mainController.CanAttack || !_mainController.AttackTarget)
            return;
        //CHECK FOR ATTACK RANGE
        if (CheckRange())
            Attack();
    }
    bool CheckRange() {
        if (!_mainController.AttackTarget)
            return false;
        //SUMMONS DONT ATTACK PLAYER
        if (_mainController.gameObject.CompareTag("Summoned") && _mainController.AttackTarget == _mainController.Player.gameObject)
            return false;
        return _mainController.Stats.Range >= _mainController.CheckDistance(_mainController.AttackTarget.transform.position);
    }
    void Attack() {
        if (_mainController.AttackTarget == null)
            return;
        _mainController.CanAttack = false;
        _mainController.Animator.SetTrigger("attack");
        _mainController.StartCoroutine(Hit());
        _mainController.StartCoroutine(SetAttack());
    }
    IEnumerator Hit() {
        yield return new WaitForSeconds(.5f);
        Health health = null;
        if (_mainController.AttackTarget && _mainController.CheckDistance(_mainController.AttackTarget.transform.position) < _mainController.Stats.Range + 1)
            _mainController.AttackTarget.TryGetComponent<Health>(out health);
        if (health != null)
            health.GetDamage(_mainController.Stats.Damage, _mainController.transform);
    }
    IEnumerator SetAttack() {
        yield return new WaitForSeconds(_mainController.Stats.AttackSpeed);
        _mainController.CanAttack = true;
    }
    
}