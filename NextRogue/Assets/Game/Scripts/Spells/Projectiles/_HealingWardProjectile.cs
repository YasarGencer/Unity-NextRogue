using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

public class _HealingWardProjectile : APlayerProjectile
{
    Transform _player;
    IDisposable _followRX;
    public override void Initialize(Vector3 mousePos, float damage, float time) {
        base.Initialize(mousePos, damage, time);
        Destroy(gameObject, time / 2);
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
        Check(collision.gameObject.tag);
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        Check(collision.gameObject.tag);
    }
    void Check(String collision) {
        if(collision == "Enemy" || collision == "EnemyProjectile") {
            _player.GetComponent<P_MainController>().Health.GainHealth(UnityEngine.Random.Range(5f, 15f));
            Destroy(gameObject);
        }
    }
}
