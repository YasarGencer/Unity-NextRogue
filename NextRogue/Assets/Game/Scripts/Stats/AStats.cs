using UnityEngine;

public abstract class AStats : ScriptableObject
{
    [Header("DETAILS")]
    public string Name;
    public string ShortDescription;
    public string Description;
    public GameObject Corpse;
    [Header("HEALTH")]
    public float MaxHealth;
    [HideInInspector]
    public float Health;
    public GameObject HitParticle;
    public GameObject EXPOrb;
    [Header("SOUND")]
    public AudioClip HitSound;
    public AudioClip HealSound;
    public AudioClip AttackSound;
    public AudioClip WalkSound;
    [Header("MOVEMENT")]
    public float Speed;
    [HideInInspector]
    public float SpeelHolder;
    [HideInInspector]
    public bool IsInvincable = false;

    public virtual void Initialize() { 
        Health = MaxHealth;
        SpeelHolder = Speed;
        IsInvincable = false; 
    }

}
