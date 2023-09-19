using UnityEngine;

[CreateAssetMenu(fileName = "HumanKnightRush", menuName = "ScriptableObjects/BossSpells/Human_KnightRush")]
public class HumanKnightRush : ANP_Spell {
    public override bool CheckConditions() { 
        if (base.CheckConditions() == false)
            return false; 

        return true;

    }
    public override void ActivateSpell() {
        base.ActivateSpell();
        JustCast(Vector3.zero); 
    }
}
