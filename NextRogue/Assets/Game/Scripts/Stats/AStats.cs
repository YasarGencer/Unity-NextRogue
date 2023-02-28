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
    [Header("MOVEMENT")]
    public float Speed;
    [HideInInspector]
    public float SpeelHolder;

    public virtual void Initialize() {
        Health = MaxHealth;
        SpeelHolder = Speed;
    }
    
}
