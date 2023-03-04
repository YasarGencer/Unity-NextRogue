using UnityEngine;

[CreateAssetMenu(fileName = "Stats", menuName = "ScriptableObjects/Stats/PlayerStats", order = 0)]
public class P_Stats : AStats { 
    [Header("Spell Set")]
    public ASpell[] Spells;
    [Header("Level")]
    public int Level;
    public int EXP, EXPRequired;
    private int _expRequired = 0;
    public override void Initialize() {
        base.Initialize();
        foreach (var item in Spells)
            item.IsInit = false;
        if (_expRequired == 0)
            _expRequired = EXPRequired;
    }
    public override void ResetStats() {
        base.ResetStats();
        Level = 0;
        EXP = 0;
        EXPRequired = _expRequired;
        _expRequired = 0;
    }
}
