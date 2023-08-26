using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct DOTInfo 
{
    public DOTType DOTType;
    [Tooltip("Damage in each cycle")] public float CycleDamage;
    [Tooltip("How many cycles till effect runs out")] public float CycleTime;
}
public enum DOTType {
    BLEED,
    BURN,
    POISON
}
