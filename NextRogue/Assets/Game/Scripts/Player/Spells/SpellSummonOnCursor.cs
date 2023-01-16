using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngineInternal;

[CreateAssetMenu(fileName = "Spells", menuName = "ScriptableObjects/Spells/SummonOnCursor", order = 1)]
public class SpellSummonOnCursor : ASpell {

    public override void Initialize(PlayerMainController mainController, int value) {
        base.Initialize(mainController, value);
    }
    public override void ActivateSpell() {
        base.ActivateSpell();
        JustCast(_mainController.Input.GetMousePos());
    }

}