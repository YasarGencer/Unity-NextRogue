using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class __ShotProjectile : AProjectile {
    public override void Initialize(Vector3 mousePos, float damage, float time, float speed) {
        base.Initialize(mousePos, damage, time, speed);
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        Destroy(gameObject, .05F);
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        Destroy(gameObject, .05f);
    }
}
