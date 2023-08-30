using UnityEngine;
[System.Serializable]
public class DOTInfo 
{
    public DOTType DOTType;
    [Tooltip("Damage in each cycle")] public float CycleDamage;
    [Tooltip("How many cycles till effect runs out")] public float CycleTime;

    public DOTInfo(DOTType dOTType, float cycleDamage, float cycleTime) {
        DOTType = dOTType;
        CycleDamage = cycleDamage;
        CycleTime = cycleTime;
    }
}
public enum DOTType {
    BLEED,
    BURN,
    POISON
}
