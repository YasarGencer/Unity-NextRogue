using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Spells", menuName = "ScriptableObjects/Spells/SummonDeadAround", order = 2)]
public class SpellSummonDeadAround : ASpell {
    public override void Initialize(PlayerMainController mainController, int value) {
        base.Initialize(mainController, value);
    }
    public override void ActivateSpell() {
        base.ActivateSpell();
        JustCast(_mainController.transform.position);
    }

}
