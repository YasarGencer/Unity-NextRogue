using System; 
using System.Collections.Generic;
using System.Linq; 
using UnityEngine;
[CreateAssetMenu(fileName = "Spells", menuName = "ScriptableObjects/Spells/SpellList", order = 0)]
public class AllSpells : ScriptableObject
{
    [SerializeField] ASpell[] SpellList;
    
    int indexer = 0;
    public ASpell GetSpell(int index) {
        return SpellList[index];
    }
    public ASpell[] GetAllSpells() {
        return SpellList;
    }
    public int GetSpellCount() { return SpellList.Length; } 
    public ASpell GetRandomSpell() {
        List<ASpell> aSpells = new List<ASpell>();

        foreach (var item in GetAllSpells())
            if (item.IsInit == false)
                aSpells.Add(item);

        var random = UnityEngine.Random.Range(0, aSpells.Count);
        return aSpells[random];
    }
    public List<ASpell> GetRandomSpell(int value) {
        List<ASpell> aSpells = new List<ASpell>();

        foreach (var item in GetAllSpells())
            if (item.IsInit == false)
                aSpells.Add(item); 

        List<int> count = new List<int>();
        for (int i = 0; i < aSpells.Count; i++)
            count.Add(i);  
        var random = count.OrderBy(a => Guid.NewGuid()).ToList();

        List<ASpell> bSpells = new List<ASpell>();
        for (int i = 0; i < 3; i++)
            bSpells.Add(aSpells[random.ElementAt(i)]);
        return bSpells;
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
