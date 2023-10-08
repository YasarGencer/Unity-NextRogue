using System.Threading.Tasks; 
using UnityEngine;

public class NP_Attack_Ranged : ANP_Attack {
    [SerializeField] GameObject _projectile;
    [SerializeField] bool _hasSerialAttacks;
    [SerializeField] int _serialAttackAmount;
    int _counter = 0;
    public override void Initialize(NP_MainController mainController) {
        base.Initialize(mainController);
        _counter = 0;
    }
    protected override void UpdateRX(long obj) {
        base.UpdateRX(obj);
    }
    protected override void Attack() {
        if (MainManager.Instance.GameManager.GamePaused)
            return;
        _attackTime = _mainController.Stats.AttackSpeed;
        StartCoroutine(_mainController.Movement.FreezeMovement(1));
        _mainController.Animator.SetTrigger("attack");
        Spawn();
        if (_hasSerialAttacks) {
            _counter++;
        }if(_serialAttackAmount < _counter) {
            _counter = 0;
            _attackTime = _mainController.Stats.AttackSpeed * _serialAttackAmount;
        }
    }
    async void Spawn() {
        await Task.Delay(750);
        if (_mainController == null)
            return;
        AudioManager.PlaySound(_mainController.Stats.AttackSound);
        _mainController.Animator.speed = 1;
        while (MainManager.Instance.GameManager.GamePaused) {
            _mainController.Animator.speed = 0;
            await Task.Delay(100);
        }
        if (_mainController == null)
            return;
        _mainController.Animator.speed = 1;
        GameObject projectile = Instantiate(_projectile, transform.position, Quaternion.identity);
        if (projectile == null)
            return;
        projectile.GetComponent<AProjectile>()
            .Initialize(
            _mainController.Target.Target.transform.position, 
            _mainController.Stats.AttackDamage, 
            _mainController.Stats.AttackSpeed * 0.75f, 
            _mainController.Stats.ProjectileSpeed,
            _mainController.Stats.DOTInfo);
    }
}
