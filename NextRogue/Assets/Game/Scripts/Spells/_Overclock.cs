using UnityEngine;

[CreateAssetMenu(fileName = "Overclock", menuName = "ScriptableObjects/Spells/Overclock")]
public class _Overclock : ASpell {

    public override void Initialize(P_MainController mainController, int value) {
        base.Initialize(mainController, value);
    }
    public override void ActivateSpell() {
        base.ActivateSpell();
        Destroy(Instantiate(Spell, _mainController.transform), 2f);
        foreach (var item in GameObject.FindGameObjectWithTag("Player").GetComponent<P_MainController>().Spells.SpellList)  
            if (item && item.IsInit && item != this)
                item.RetrieveCooldown(); 
    } 
}