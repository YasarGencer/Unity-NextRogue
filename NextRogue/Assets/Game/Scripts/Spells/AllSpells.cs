using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Spells", menuName = "ScriptableObjects/Spells/SpellList", order = 0)]
public class AllSpells : ScriptableObject
{
    [SerializeField] ASpell[] SpellList;
    [SerializeField] ASpell[] BasicList;
    public ASpell GetSpell(int index) {
        return SpellList[index];
    }
    public ASpell GetBasic(int index) {
        return BasicList[index];
    }
    public void Initialize() {
        foreach (var item in SpellList) {
            item.IsInit= false;
        }
        foreach (var item in BasicList) {
            item.IsInit = false;
        }
    }
}
