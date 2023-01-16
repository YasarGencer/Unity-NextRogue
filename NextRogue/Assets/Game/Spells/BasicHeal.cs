using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Spells", menuName = "ScriptableObjects/Basics/BasicHeal", order = 1)]
public class BasicHeal : ASpell
{
    public override void Initialize(PlayerMainController mainController, int value) {
        base.Initialize(mainController, value);
    }
    public override void ActivateSpell() {
        base.ActivateSpell();
        _mainController.Health.GainHealth(Damage);
    }
}
