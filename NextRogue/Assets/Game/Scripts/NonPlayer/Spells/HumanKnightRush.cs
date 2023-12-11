using UnityEngine;

[CreateAssetMenu(fileName = "HumanKnightRush", menuName = "ScriptableObjects/BossSpells/Human_KnightRush")]
public class HumanKnightRush : ANP_Spell {
    public override bool CheckConditions(ANP_MainController mainController) { 
        if (base.CheckConditions(mainController) == false)
            return false;
        //if (_mainController.Stats.Health > _mainController.Stats.MaxHealth * (5 / 8))
        //    return false;
        return true;

    }
    public override void ActivateSpell() {
        base.ActivateSpell();
        JustCast(Vector2.zero);
    }
}
