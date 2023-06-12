using UniRx.Triggers;
using UnityEngine;

[CreateAssetMenu(fileName = "DivineHeal", menuName = "ScriptableObjects/EnemySpells/DivineHeal")]
public class DivineHeal : ANP_Spell
{
    public override bool CheckConditions() {
        if (_mainController == null)
            return false;
        if (_mainController.Target.Target == null)
            return false;
        if (_mainController.Distance(_mainController.Target.Target.transform) > UseRange)
            return false;
        if (_mainController.Stats.Health > _mainController.Stats.MaxHealth / 2)
            return false;
        return true;
        
    }
    public override void ActivateSpell() {
        base.ActivateSpell();
        JustCast(_mainController.transform.position, true);
        _mainController.Health.GainHealth(_mainController.Stats.MaxHealth / 4);
    }
}
