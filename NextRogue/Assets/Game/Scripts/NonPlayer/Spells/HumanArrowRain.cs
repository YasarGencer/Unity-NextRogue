using UnityEngine;

[CreateAssetMenu(fileName = "HumanArrowRain", menuName = "ScriptableObjects/BossSpells/Human_ArrowRain")]
public class HumanArrowRain : ANP_Spell {
    public override bool CheckConditions(ANP_MainController mainController) {
        if (base.CheckConditions(mainController) == false)
            return false;
        //if (_mainController.Stats.Health > _mainController.Stats.MaxHealth * (3 / 4)) 
        //    return false;
        return true;

    }
    public override void ActivateSpell() {
        base.ActivateSpell();
        JustCast(MainManager.Instance.Player.GetChild(0).transform.position);
    }
}
