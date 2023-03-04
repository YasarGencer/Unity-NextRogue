using UnityEngine;

public class _DualShotProjectile : AP_Projectile { 
    [SerializeField] GameObject _explosion;

    public override void Initialize(Vector3 mousePos, float damage, float time, float speed) {
        base.Initialize(mousePos, damage, time, speed);
    }
    void Explode() {
        Damager damager = Instantiate(_explosion, transform.position, Quaternion.identity).GetComponent<Damager>();
        damager.Initialize(_damage);
        Destroy(damager.gameObject, 1f);
        Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        Explode();
    }
}
