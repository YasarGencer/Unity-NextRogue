using UnityEngine;
using UniRx;
using System;

public class _HealingWardProjectile : AP_Projectile
{
    [SerializeField] int time;
    [SerializeField] bool isInvincable;
    Transform _player;
    IDisposable _followRX;
    public override void Initialize(Vector3 mousePos, float damage, float time, float speed) {
        base.Initialize(mousePos, damage, this.time, speed);
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        StartFollow();
    }
    void StartFollow() {
        _followRX?.Dispose();
        _followRX = Observable.EveryUpdate().TakeUntilDisable(this).Subscribe(FollowRX);
    }
    void StoplFollow() {
        _followRX?.Dispose();
        if (isInvincable)
            _player.GetComponent<P_MainController>().Stats.IsInvincable = false;
    }
    private void OnDestroy() {
        StoplFollow(); 
    }
    void FollowRX(long obj) {
        this.transform.position = _player.position;
        if(isInvincable)
            _player.GetComponent<P_MainController>().Stats.IsInvincable= true;
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
            PlaySound();
        }
    }
}
