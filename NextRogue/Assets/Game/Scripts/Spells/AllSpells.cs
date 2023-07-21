using System; 
using System.Collections.Generic;
using System.Linq; 
using UnityEngine;
[CreateAssetMenu(fileName = "Spells", menuName = "ScriptableObjects/Spells/SpellList", order = 0)]
public class AllSpells : ScriptableObject
{ 
    [SerializeField] SpellHolder[] SpellHolderList;
    int indexer = 0;
    public ASpell GetSpell(int index) {
        return SpellHolderList[index].Spell;
    }  
    public SpellHolder[] GetSpellHolderList() {
        return SpellHolderList;
    }
    public List<SpellHolder> GetRandomSpell(int value) {
        List<SpellHolder> aSpells = new List<SpellHolder>();

        foreach (var item in SpellHolderList)
            if (item.Spell.IsChoosen == false)
                aSpells.Add(item); 

        List<int> count = new List<int>();
        for (int i = 0; i < aSpells.Count; i++)
            count.Add(i);  
        var random = count.OrderBy(a => Guid.NewGuid()).ToList();

        List<SpellHolder> bSpells = new List<SpellHolder>();
        for (int i = 0; i < 3; i++)
            bSpells.Add(aSpells[random.ElementAt(i)]);
        return bSpells;
    }
    public void Initialize() {
        foreach (var item in SpellHolderList) {
            ASpell spell = item.Spell;
            spell.IsInit = false;
            spell.IsChoosen= false;
            item.Index = indexer;
            spell.KeyIndex = -1;
            indexer++;
        }
    }
}
[System.Serializable]
public class SpellHolder { 
    public int Index;
    public ASpell Spell;
    public ASpell EnhancedSpell;
    public SpellType Type;
    public bool IsChallangeDone;
}
public enum SpellType {
    BerserkerThrow,
    DualShot,
    HealingWard,
    IceBarrage,
    Overclock,
    SubjugateWill,
    ImpailingShot,
    SpikeTrap
}