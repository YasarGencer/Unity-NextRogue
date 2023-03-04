using UnityEngine;

[CreateAssetMenu(fileName = "Stats", menuName = "ScriptableObjects/Stats/NonPlayerStats", order = 1)]
public class NP_Stats : AStats {
    [Header("NOTICE")]
    public float NoticeRange;
    [Header("DAMAGE")]
    public float AttackDamage;
    public float AttackRange;
    public float AttackSpeed;
    public float ProjectileSpeed;
    [Header("ONLY FOR SUMMONS")]
    public float LifeSpan;
    public override void Initialize() {
        base.Initialize();
    }
} 
