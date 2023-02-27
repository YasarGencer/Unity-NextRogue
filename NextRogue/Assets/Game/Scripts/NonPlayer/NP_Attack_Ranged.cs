using UnityEngine;

public class NP_Attack_Ranged : ANP_Attack {
    [SerializeField] GameObject _projectile;
    protected override void UpdateRX(long obj) {
        if (_mainController.Target.Target == null)
            return;
        if (_mainController.Distance(_mainController.Target.Target.transform) < _mainController.Stats.AttackRange)
            if(ShootRay())
                Attack();
    }
    private void Attack() {
        StartCoroutine(AttackLimiter());
        StartCoroutine(_mainController.Movement.FreezeMovement(1));
        _mainController.Animator.SetTrigger("Charge");
        Spawn();
    }
    void Spawn() {
        Instantiate(_projectile, transform.position, Quaternion.identity).GetComponent<ANP_Projectile>()
            .Initialize(_mainController.Target.Target.transform.position, _mainController.Stats.AttackDamage, _mainController.Stats.AttackSpeed * 0.75f);
    }
}
