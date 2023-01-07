using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBasicRangedProjectile : APlayerBasicProjectile
{
    Rigidbody2D _rb;
    [SerializeField] GameObject _explosion;

    public override void Initialize(Vector3 mousePos, float damage) {
        base.Initialize(mousePos, damage);
        _rb = GetComponent<Rigidbody2D>();
        Move();
    }
    void Move() {
        _rb.AddForce(transform.right * 1000);
    }
    void Explode() {
        Damager damager = Instantiate(_explosion, transform.position, Quaternion.identity).GetComponent<Damager>();
        damager.Initialize(_damage);
        Destroy(damager.gameObject, 1f);
        Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        switch (collision.gameObject.tag) {
            case "Enviroment":
                break;
            case "Enemy":
                break;
            default:
                break;
        }
        Explode();
    }
}
