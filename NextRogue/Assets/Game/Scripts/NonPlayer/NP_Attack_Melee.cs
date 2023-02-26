public class NP_Attack_Melee : ANP_Attack {
    protected override void UpdateRX(long obj) {
        if (_mainController.Target.Target == null)
            return;
        if (_mainController.Distance(_mainController.Target.Target.transform) < _mainController.Stats.AttackRange)
            Attack();
    }
    private void Attack() {
        StartCoroutine(_mainController.Movement.FreezeMovement(1));
        Invoke("CheckHit", .25f);
        _mainController.Animator.SetTrigger("attack");
        StartCoroutine(AttackLimiter());
    }
    void CheckHit() {
        if (_mainController.Target.Target == null)
            return;
        if (_mainController.Distance(_mainController.Target.Target.transform) < _mainController.Stats.AttackRange)
            _mainController.Target.Target.GetComponent<Health>().GetDamage(_mainController.Stats.AttackDamage,transform);
    }
}
