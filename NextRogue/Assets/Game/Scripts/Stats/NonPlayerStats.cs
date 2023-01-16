using System.Collections;
using System.Collections.Generic;
using UniRx.Triggers;
using UnityEngine;

public class NonPlayerStats : AStats {
    [Header("DAMAGE")]
    public float Damage;
    public float Range;
    public float AttackSpeed;
    [Header("ONLY FOR SUMMONS")]
    public float EnemyRange;
    public float LifeSpan;
    public override void Initialize() {
        base.Initialize();
    }
    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(transform.position, Range);
        Gizmos.DrawWireSphere(transform.position, EnemyRange);
    }
} 
