using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AStats : MonoBehaviour
{
    [Header("DETAILS")]
    public string Name;
    public string ShortDescription;
    public string Description;
    [Header("HEALTH")]
    public float Health;
    public float MaxHealth;
    [Header("MOVEMENT")]
    public float Speed;

    public virtual void Initialize() {
        Health = MaxHealth;
    }
}
