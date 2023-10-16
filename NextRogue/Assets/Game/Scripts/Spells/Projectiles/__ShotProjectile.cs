using UnityEngine;

public class __ShotProjectile : AP_Projectile {
    [SerializeField] bool _setActiveOnCollision = false;
    public override void Initialize(Vector3 mousePos, float damage, float time, float speed, DOTInfo dotInfo) {
        base.Initialize(mousePos, damage, time, speed, dotInfo);

        if (CompareTag("EnemyProjectile")) { 
            gameObject.layer = 13;
            gameObject.tag = "EnemyProjectile";
        } else { 
            gameObject.layer = 6;
            gameObject.tag = "PlayerProjectile";
        } 
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        PlaySound();
        gameObject.SetActive(_setActiveOnCollision);  
    } 
}
