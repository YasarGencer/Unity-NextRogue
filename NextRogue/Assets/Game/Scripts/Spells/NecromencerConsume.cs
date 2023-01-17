using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NecromancerConsume", menuName = "ScriptableObjects/Spells/NecromancerConsume")]
public class NecromencerConsume : ASpell
{
    public override void Initialize(PlayerMainController mainController, int value) {
        base.Initialize(mainController, value);
    }
    public override void ActivateSpell() {
        base.ActivateSpell();
        _mainController.Health.GainHealth(Damage);
    }
}
