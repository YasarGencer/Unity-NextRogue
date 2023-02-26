using UnityEngine;

public class ArcaneArcher_Projectile_shard : MonoBehaviour
{
    Rigidbody2D _rb;
    Damager _damager;
    public void Initialize(float damage) {
        _rb = GetComponent<Rigidbody2D>();
        _damager = GetComponent<Damager>();

        _damager.Initialize(damage);

        Invoke("Move",.5f);
    }
    void Move() {
        _rb.AddForce(transform.right * 750 * _rb.mass);
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        Destroy(gameObject);
    }
}
