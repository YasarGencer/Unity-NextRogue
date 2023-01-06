using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBasicRangedProjectile : APlayerBasicProjectile
{
    Rigidbody2D _rb;
    [SerializeField] GameObject _explosion;
    public override void Initialize(Vector3 mousePos) {
        base.Initialize(mousePos);
        _rb = GetComponent<Rigidbody2D>();
        Move();
    }
    void Move() {
        _rb.AddForce(transform.right * 1000);
    }
    void Explode() {
        Destroy(Instantiate(_explosion, transform.position, Quaternion.identity), 1f);
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
