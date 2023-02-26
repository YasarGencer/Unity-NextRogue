using UnityEngine;

[CreateAssetMenu(fileName = "SpellJustCastOnCursor", menuName = "ScriptableObjects/Spells/SpellJustCastOnCursor")]
public class SpellJustCastOnCursor : ASpell {

    public override void Initialize(P_MainController mainController, int value) {
        base.Initialize(mainController, value);
    }
    public override void ActivateSpell() {
        base.ActivateSpell();
        JustCast(_mainController.Input.GetMouseWolrdPos());
    }

}