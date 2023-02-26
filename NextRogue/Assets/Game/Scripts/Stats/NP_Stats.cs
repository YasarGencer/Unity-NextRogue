using UnityEngine;

public class NP_Stats : AStats {
    [Header("NOTICE")]
    public float NoticeRange;
    [Header("DAMAGE")]
    public float AttackDamage;
    public float AttackRange;
    public float AttackSpeed;
    [Header("ONLY FOR SUMMONS")]
    public float LifeSpan;
    public override void Initialize() {
        base.Initialize();
    }
    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(transform.position, NoticeRange);
        Gizmos.DrawWireSphere(transform.position, AttackRange);
    }
} 
