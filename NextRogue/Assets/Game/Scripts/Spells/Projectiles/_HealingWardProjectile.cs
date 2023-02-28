using UnityEngine;
using UniRx;
using System;

public class _HealingWardProjectile : AP_Projectile
{
    Transform _player;
    IDisposable _followRX;
    public override void Initialize(Vector3 mousePos, float damage, float time) {
        base.Initialize(mousePos, damage, 1);
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        StartFollow();
    }
    void StartFollow() {
        _followRX?.Dispose();
        _followRX = Observable.EveryUpdate().TakeUntilDisable(this).Subscribe(FollowRX);
    }
    void StoplFollow() {
        _followRX?.Dispose();
    }
    void FollowRX(long obj) {
        this.transform.position = _player.position;
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        Check(collision.gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        Check(collision.gameObject);
    }
    void Check(GameObject collision) {
        if(collision.tag == "EnemyProjectile") {
            Destroy(collision.gameObject);
            _player.GetComponent<P_MainController>().Health.GainHealth(_damage);
        }
    }
}
