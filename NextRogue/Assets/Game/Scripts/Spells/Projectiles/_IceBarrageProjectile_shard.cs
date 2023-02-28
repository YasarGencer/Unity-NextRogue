using UnityEngine;

public class _IceBarrageProjectile_shard : AP_Projectile { 
    Damager _damager;
    Animator _animator;
    public override void Initialize(Vector3 targetPos, float damage, float time) {
        base.Initialize(Vector3.zero, damage, time); 
        _damager = GetComponent<Damager>();
        _animator = GetComponent<Animator>();

        _damager.Initialize(damage);

        _speed = 500;
        Invoke("Mover", .5f);
    }
    void Mover() {
        if (_isPaused)
            return;
        Move(_speed);
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        if(_animator)
            _animator.SetTrigger("hit");

        Destroy(gameObject,.4f);
    }
}
