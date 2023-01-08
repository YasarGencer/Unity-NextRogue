using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class PlayerSpells : MonoBehaviour
{
    PlayerMainController _mainController;
    [SerializeField] ASpell[] _spellList;
    public AllSpells AllSpellList;
    public void Initialize(PlayerMainController mainController) {
        _mainController= mainController;
        _spellList = new ASpell[5];
        AllSpellList.Initialize();
        //Temp
        SetSpell(0, 0);
        SetSpell(0, 1);
    }
    public void Spell(int value) {
        _spellList[value].Initialize(_mainController);
    }
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
    }
    public bool CheckSpellKeyIndex(int keyIndex) {
        return _spellList[keyIndex] == null;
    }
}
