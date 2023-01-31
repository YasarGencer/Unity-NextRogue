using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _IceBarrageProjectile_shard : MonoBehaviour {
    Rigidbody2D _rb;
    Damager _damager;
    Animator _animator;
    public void Initialize(Vector3 mousePos, float damage, float time) {
        _rb = GetComponent<Rigidbody2D>();
        _damager = GetComponent<Damager>();
        _animator = GetComponent<Animator>();

        _damager.Initialize(damage);

        Move();
    }
    void Move() {
        _rb.AddForce(transform.right * 500 * _rb.mass);
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        if(_animator)
            _animator.SetTrigger("hit");

        Destroy(gameObject,.4f);
    }
}
