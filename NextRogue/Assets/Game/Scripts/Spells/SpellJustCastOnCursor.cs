using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngineInternal;

[CreateAssetMenu(fileName = "SpellJustCastOnCursor", menuName = "ScriptableObjects/Spells/SpellJustCastOnCursor")]
public class SpellJustCastOnCursor : ASpell {

    public override void Initialize(PlayerMainController mainController, int value) {
        base.Initialize(mainController, value);
    }
    public override void ActivateSpell() {
        base.ActivateSpell();
        JustCast(_mainController.Input.GetMouseWolrdPos());
    }

}