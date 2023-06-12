using UnityEngine;

public class NecromancerReaperProjectile : AP_Projectile {
    public override void Initialize(Vector3 mousePos, float damage, float time, float speed) {
        base.Initialize(mousePos, damage, time * 2, speed);
    }
}
