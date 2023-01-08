using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngineInternal;

[CreateAssetMenu(fileName = "Spells", menuName = "ScriptableObjects/Spells/SummonOnCursor", order = 1)]
public class SpellSummonOnCursor : ASpell {

    public override void Initialize(PlayerMainController mainController) {
        base.Initialize(mainController);
    }
    public override void ActivateSpell() {
        base.ActivateSpell();
    }

}