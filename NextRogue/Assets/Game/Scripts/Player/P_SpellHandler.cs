using System.Linq;
using UnityEngine;

public class P_SpellHandler : MonoBehaviour
{
    P_MainController _mainController;
    ASpell[] _spellList;
    public AllSpells AllSpellList;
    public void Initialize(P_MainController mainController) {
        _mainController= mainController;
        _spellList = new ASpell[9];

        AllSpellList.Initialize();

        //SET 0,1,2,3 AS WIZARD SPESIFIC SPELLS
        for (int i = 0; i < _mainController.Stats.Spells.Count(); i++)
            SetSpell(i, _mainController.Stats.Spells[i]);

        // 4,5,6,7,8 ARE IN GAME SPELLS
        SetSpell(4, AllSpellList.GetSpell(0));
        SetSpell(5, AllSpellList.GetSpell(1));
        SetSpell(6, AllSpellList.GetSpell(2));
        //SetSpell(7, AllSpellList.GetSpell(3));
    }
    public void Spell(int value) {
        _spellList[value].Initialize(_mainController, value); 
    }
    public void SetSpell(int keyIndex, ASpell spell) {
        _spellList[keyIndex] = spell;
        if(_mainController.UI)
            _mainController.UI.SetSkillIcon(_mainController.UI.spellIconList, keyIndex, _spellList[keyIndex]);
    }
    public bool CheckSpellKeyIndex(int keyIndex) {
        return _spellList[keyIndex] == null;
    }
    /*
    public void SetSpell(int keyIndex, int spellIndex) {
        if (!CheckSpellKeyIndex(keyIndex)) {
            ASpell spell = _spellList[keyIndex];
            for (int i = 0; i < _spellList.Length - 1; i++)
                if (CheckSpellKeyIndex(i)) {
                    _spellList[keyIndex] = null;
                    _spellList[i] = spell;
                    break;
                }
        }
        _spellList[keyIndex] = AllSpellList.GetSpell(spellIndex);
        _mainController.UI.SetSkillIcon(_mainController.UI.spellIconList, keyIndex, _spellList[keyIndex]);
    }
    */
}
