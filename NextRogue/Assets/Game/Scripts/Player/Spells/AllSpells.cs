using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Spells", menuName = "ScriptableObjects/Spells/SpellList", order = 0)]
public class AllSpells : ScriptableObject
{
    [SerializeField] ASpell[] SpellList;
    public ASpell GetSpell(int index) {
        return SpellList[index];
    }
    public void Initialize() {
    }
}
