using UnityEngine;

public class NP_Attack_Ranged : ANP_Attack {
    [SerializeField] GameObject _projectile;
    protected override void UpdateRX(long obj) {
        base.UpdateRX(obj);
    }
    protected override void Attack() {
        if (MainManager.Instance.GameManager.GamePaused)
            return;
        _attackTime = _mainController.Stats.AttackSpeed;
        StartCoroutine(_mainController.Movement.FreezeMovement(1));
        _mainController.Animator.SetTrigger("Charge");
        Spawn();
    }
    void Spawn() {
        Instantiate(_projectile, transform.position, Quaternion.identity).GetComponent<AProjectile>()
            .Initialize(_mainController.Target.Target.transform.position, _mainController.Stats.AttackDamage, _mainController.Stats.AttackSpeed * 0.75f, _mainController.Stats.ProjectileSpeed);
    }
}
