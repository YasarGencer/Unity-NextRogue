using UnityEngine;

[CreateAssetMenu(fileName = "Stats", menuName = "ScriptableObjects/Stats/PlayerStats", order = 0)]
public class P_Stats : AStats {
    [Header("Spell Set")]
    public ASpell[] Spells;
    [Header("Level")]
    public int Level;
    public int EXP, EXPRequired;
    public override void Initialize() {
        base.Initialize();
        foreach (var item in Spells)
            item.IsInit = false;
    }
}
