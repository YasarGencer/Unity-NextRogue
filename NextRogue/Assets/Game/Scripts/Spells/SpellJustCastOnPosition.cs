using UnityEngine;

[CreateAssetMenu(fileName = "JustCastOnPosition", menuName = "ScriptableObjects/Spells/JustCastOnPosition")]
public class SpellJustCastOnPosition : ASpell {

    public override void Initialize(P_MainController mainController, int value) {
        base.Initialize(mainController, value);
    }
    public override void ActivateSpell() {
        base.ActivateSpell();
        JustCast(_mainController.transform.position);
    }

}