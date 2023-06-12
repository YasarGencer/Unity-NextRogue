using System.Linq;
using UnityEngine; 

public class P_SpellHandler : MonoBehaviour {
    bool _isInit;
    P_MainController _mainController;
    ASpell[] _spellList;
    public ASpell[] SpellList { get { return _spellList; } }
    public void Initialize(P_MainController mainController) {
        if(_spellList != null && _spellList.Length > 0)
            foreach (var item in _spellList)
                if(item != null && item.IsInit)
                    item.RetrieveCooldown(); 
        if (_isInit)
            return;
        _isInit = true;
        _mainController = mainController;
        _spellList = new ASpell[9]; 

        //SET 0,1,2,3 AS WIZARD SPESIFIC SPELLS
        for (int i = 0; i < _mainController.Stats.Spells.Count(); i++)
            SetSpell(i, _mainController.Stats.Spells[i]); 
    }
    public void Spell(int value) {
        _spellList[value]?.Initialize(_mainController, value); 
    }
    public void SetSpell(int keyIndex, ASpell spell) { 
        _spellList[keyIndex] = spell;
        _spellList[keyIndex].IsChoosen = true;
        if(_mainController.UI)
            _mainController.UI.SetSkillIcon(_mainController.UI.spellIconList, keyIndex, _spellList[keyIndex]);
    }
    public ASpell GetSpell(int value) {
        if (CheckSpellKeyIndex(value))
            return _spellList[value];
        else
            return null;
    }
    public bool CheckSpellKeyIndex(int keyIndex) {
        return _spellList[keyIndex] != null;
    } 
    public void DeleteMainSpells() {
        foreach (var item in _spellList)
            if (item != null) {
                item.RetrieveCooldown();
                item.IsInit = false;
            } 
        _spellList = new ASpell[9];
        _isInit = false;
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
    private void OnDestroy() {
        
    }
}
