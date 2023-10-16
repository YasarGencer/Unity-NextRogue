using UnityEngine;
using UniRx;
using System;

public class _HealingWardProjectile : AP_Projectile
{
    [SerializeField] int time;
    [SerializeField] bool isInvincable;
    [SerializeField] bool isFollow;

    public static int numberOfBlockedAttack=0;
    Transform _player;
    IDisposable _followRX;
    public override void Initialize(Vector3 mousePos, float damage, float time, float speed, DOTInfo dotInfo) {
        base.Initialize(Vector3.zero, damage, this.time, speed, dotInfo);
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        if (isFollow)
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
            numberOfBlockedAttack = PlayerPrefs.GetInt(nameof(numberOfBlockedAttack));
            numberOfBlockedAttack++;
            PlayerPrefs.SetInt(nameof(numberOfBlockedAttack), numberOfBlockedAttack);
        }
        if (PlayerPrefs.GetInt(nameof(numberOfBlockedAttack)) >= 1)
        {
            MainManager.Instance.GameManager.ChallangeManager.RegisterChallangeDone(SpellType.HealingWard);

        }
    }
}
