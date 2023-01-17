using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Spells", menuName = "ScriptableObjects/Spells/SpellList", order = 0)]
public class AllSpells : ScriptableObject
{
    [SerializeField] ASpell[] SpellList;
    
    int indexer = 0;
    public ASpell GetSpell(int index) {
        return SpellList[index];
    }
    public void Initialize() {
        foreach (var item in SpellList) {
            item.IsInit= false;
            item.Index = indexer;
            item.KeyIndex = -1;
            indexer++;
        }
    }
}
