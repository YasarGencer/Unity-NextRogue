using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodshaperBloodShotParticle : AP_Projectile { 
    public override void Initialize(Vector3 mousePos, float damage, float time, float speed, DOTInfo dotInfo) {
        base.Initialize(mousePos, damage, time, speed, dotInfo);
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        PlaySound();
        SetVelocity(Vector2.zero);
        Destroy(_rb);
        Destroy(GetComponent<CircleCollider2D>());
        Destroy(gameObject, .2f);
    }
}
