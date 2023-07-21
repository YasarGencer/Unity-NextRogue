using UnityEngine;
using System.Threading.Tasks;

public class NP_Attack_Melee : ANP_Attack {
    protected override void UpdateRX(long obj) {
        base.UpdateRX(obj);
    }
    protected override void Attack() { 
        if (MainManager.Instance.GameManager.GamePaused)
            return;
        _isAttacking = true;
        _mainController.Movement.Freeze();
        CheckHit();
        _mainController.Animator.SetTrigger("attack");
    } 
    async void CheckHit() { 
        await Task.Delay(250); 
        if (_mainController.Target.Target == null)
            return; 
        if(_mainController == null)  
            return;
        while (MainManager.Instance.GameManager.GamePaused) { 
            _attackTime = _mainController.Stats.AttackSpeed;
            _mainController.Animator.speed = 0;
            await Task.Delay(100);
            if (_mainController == null)
                return;
        }
        _mainController.Movement.UnFreeze();
        _attackTime = _mainController.Stats.AttackSpeed; 
        _isAttacking = false;
        if (_mainController.Target.Target == null || _mainController == null) 
            return;
        if (_mainController.Distance(_mainController.Target.Target.transform) < _mainController.Stats.AttackRange)
            _mainController.Target.Target.GetComponent<Health>().GetDamage(_mainController.Stats.AttackDamage, transform);
        if (_mainController.Stats.AttackSound != null)
            AudioManager.PlaySound(_mainController.Stats.AttackSound);
    }
}
