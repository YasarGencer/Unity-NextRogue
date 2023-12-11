using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;


public class NP_Attack_RedBandit : NP_Attack_Melee
{

    public async override void Hit()
    {
        if (IsUsýngSpell())
            return;
        if (_mainController.Target.Target == null)
            return;
        if (_mainController == null)
            return;
        while (MainManager.Instance.GameManager.GamePaused)
        {
            _attackTime = _mainController.Stats.AttackSpeed;
            _mainController.Animator.speed = 0;
            await Task.Delay(100);
            if (_mainController == null)
                return;
        }
        if (_mainController.Target.Target == null || _mainController == null)
            return;
        if (_mainController.Distance(_mainController.Target.Target.transform) < _mainController.Stats.AttackRange)
        {
            _mainController.Target.Target.GetComponent<Health>().GetDamage(_mainController.Stats.AttackDamage, transform);
            MainManager.Instance.EventManager.RunOnCoinChange(-1);

        }
        if (_mainController.Stats.AttackSound != null)
            AudioManager.PlaySound(_mainController.Stats.AttackSound);
    }
}
