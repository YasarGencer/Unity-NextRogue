using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngineInternal;

[CreateAssetMenu(fileName = "JustCastOnPosition", menuName = "ScriptableObjects/Spells/JustCastOnPosition")]
public class SpellJustCastOnPosition : ASpell {

    public override void Initialize(PlayerMainController mainController, int value) {
        base.Initialize(mainController, value);
    }
    public override void ActivateSpell() {
        base.ActivateSpell();
        JustCast(_mainController.transform.position);
    }

}