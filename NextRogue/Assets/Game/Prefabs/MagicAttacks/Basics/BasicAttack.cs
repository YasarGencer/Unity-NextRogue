using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Spells", menuName = "ScriptableObjects/Basics/BasicAttack", order = 1)]
public class BasicAttack : ASpell {
    public override void Initialize(PlayerMainController mainController, int value) {
        base.Initialize(mainController, value);
    }
    public override void ActivateSpell() {
        base.ActivateSpell();
        Instantiate(
            Spell,
            _mainController.gameObject.transform.position,
            Quaternion.identity
            ).GetComponent<APlayerBasicProjectile>()
            .Initialize(_mainController.Input.GetMousePos(),
            Damage);
    }
}
