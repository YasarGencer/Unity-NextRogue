using UnityEngine;

[CreateAssetMenu(fileName = "Stats", menuName = "ScriptableObjects/Stats/PlayerStats", order = 0)]
public class P_Stats : AStats {
    [Header("Spell Set")]
    public ASpell[] Spells;
    public override void Initialize() {
        base.Initialize();
        foreach (var item in Spells)
            item.IsInit = false;
    } 
}
