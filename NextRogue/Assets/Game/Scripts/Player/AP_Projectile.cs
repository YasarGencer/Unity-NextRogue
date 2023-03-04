using UnityEngine;

public abstract class AP_Projectile : AProjectile {
    public override void Initialize(Vector3 targetPos, float damage, float time, float speed) {
        base.Initialize(targetPos, damage, time, speed);
    }
}
