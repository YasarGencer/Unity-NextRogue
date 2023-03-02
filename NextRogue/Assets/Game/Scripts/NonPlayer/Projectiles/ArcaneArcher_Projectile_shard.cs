using UnityEditor;
using UnityEngine;

public class ArcaneArcher_Projectile_shard : ANP_Projectile
{
    Damager _damager;
    public override void Initialize(Vector3 targetPos, float damage, float time) {
        base.Initialize(Vector3.zero, damage, time);
        _damager = GetComponent<Damager>();

        _damager.Initialize(damage);
        _speed = 750;
        Invoke("Mover",.5f);
    }
    void Mover() {
        if(MainManager.Instance.GameManager.GamePaused)
            return;
        Move(_speed);
    }
    protected override void Move(float speed) {
        base.Move(speed);
    }
    private void OnCollisionEnter2D(Collision2D collision) { 
        Destroy(gameObject);
    }
}
