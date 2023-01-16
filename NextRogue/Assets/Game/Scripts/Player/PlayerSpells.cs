using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class PlayerSpells : MonoBehaviour
{
    PlayerMainController _mainController;
    [SerializeField] ASpell[] _spellList;
    [SerializeField] ASpell[] _basicList;
    public AllSpells AllSpellList;
    public void Initialize(PlayerMainController mainController) {
        _mainController= mainController;
        _spellList = new ASpell[5];
        _basicList = new ASpell[4];
        AllSpellList.Initialize();
        //Temp
        SetBasic(0,0);
        SetBasic(1,1);
        SetBasic(2,2);
        SetBasic(3,3);

        //(0, 0);
        SetSpell(0, 0);
        SetSpell(1, 1);
    }
    public void Spell(int value) {
        _spellList[value].Initialize(_mainController, value);
    }
    public void Basic(int value) {
        _basicList[value].Initialize(_mainController, value);
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
        _mainController.UI.SetSkillIcon(_mainController.UI.spellIconList, keyIndex, _spellList[keyIndex]);
    }
    public void SetBasic(int keyIndex, int spellIndex) {
        if (!CheckSpellKeyIndex(keyIndex)) {
            ASpell spell = _basicList[keyIndex];
            for (int i = 0; i < _basicList.Length - 1; i++)
                if (CheckSpellKeyIndex(i)) {
                    _basicList[keyIndex] = null;
                    _basicList[i] = spell;
                    break;
                }
        }
        _basicList[keyIndex] = AllSpellList.GetBasic(spellIndex);
        _mainController.UI.SetSkillIcon(_mainController.UI.basicIconList, keyIndex, _basicList[keyIndex]);
    }
    public bool CheckSpellKeyIndex(int keyIndex) {
        return _spellList[keyIndex] == null;
    }
}
