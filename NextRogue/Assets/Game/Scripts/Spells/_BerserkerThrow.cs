using UnityEngine;

[CreateAssetMenu(fileName = "BerserkerThrow", menuName = "ScriptableObjects/Spells/BerserkerThrow")]
public class _BerserkerThrow : ASpell {

    public override void Initialize(P_MainController mainController, int value) {
        base.Initialize(mainController, value);
    }
    public override void ActivateSpell() {
        base.ActivateSpell();
        var projectile = JustCast(_mainController.transform.position).GetComponent<_BerserkerThrowProjectile>();
        projectile.BerserkThrow = this;
    }

}