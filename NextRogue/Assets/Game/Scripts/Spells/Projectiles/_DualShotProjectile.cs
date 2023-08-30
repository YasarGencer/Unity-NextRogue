using UnityEngine;

public class _DualShotProjectile : AP_Projectile { 
    [SerializeField] GameObject _explosion;

    public override void Initialize(Vector3 mousePos, float damage, float time, float speed, DOTInfo dotInfo) {
        base.Initialize(mousePos, damage, time, speed, dotInfo);
    }
    void Explode() {
        PlaySound();
        Damager damager = Instantiate(_explosion, transform.position, Quaternion.identity).GetComponent<Damager>();
        damager.Initialize(_damage, _dotInfo);
        Destroy(damager.gameObject, 1f);
        Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        Explode();
    }
}
