using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NP_Attack_Melee : ANP_Attack {
    protected override void UpdateRX(long obj) {
        if (_mainController.Target.Target == null)
            return;
        if (_mainController.Distance(_mainController.Target.Target.transform) < _mainController.Stats.AttackRange)
            Attack();
    }
    private void Attack() {
        Invoke("CheckHit", .75f);
        _mainController.Animator.SetTrigger("attack");
        AttackLimiter();
    }
    void CheckHit() {
        if (_mainController.Target.Target == null)
            return;
        if (_mainController.Distance(_mainController.Target.Target.transform) < _mainController.Stats.AttackRange)
            _mainController.Target.Target.GetComponent<Health>().GetDamage(_mainController.Stats.AttackDamage,transform);
    }
}
