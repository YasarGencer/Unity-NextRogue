using UnityEngine;

public class __ShotProjectile : AProjectile {
    public override void Initialize(Vector3 mousePos, float damage, float time, float speed) {
        base.Initialize(mousePos, damage, time, speed);
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        PlaySound();
        gameObject.SetActive(false); 
    } 
}
