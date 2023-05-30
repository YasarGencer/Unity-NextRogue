public class NP_Attack_Melee : ANP_Attack {
    protected override void UpdateRX(long obj) {
        base.UpdateRX(obj);
    }
    protected override void Attack() { 
        if (MainManager.Instance.GameManager.GamePaused)
            return;

        StartCoroutine(_mainController.Movement.FreezeMovement(1));
        Invoke("CheckHit", .25f);
        _mainController.Animator.SetTrigger("attack");
        _attackTime = _mainController.Stats.AttackSpeed;
    }
    void CheckHit() { 
        if (MainManager.Instance.GameManager.GamePaused)
            return;
        AudioManager.PlaySound(_mainController.Stats.AttackSound);
        if (_mainController.Target.Target == null)
            return;
        if (_mainController.Distance(_mainController.Target.Target.transform) < _mainController.Stats.AttackRange)
            _mainController.Target.Target.GetComponent<Health>().GetDamage(_mainController.Stats.AttackDamage,transform);
    }
}
