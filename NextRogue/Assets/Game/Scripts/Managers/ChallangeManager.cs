using UnityEngine; 

public class ChallangeManager : MonoBehaviour
{
    AllSpells _spells { get { return MainManager.Instance.GameManager.AllSpells; } }
    public void Initialize() {
        foreach (var item in _spells.GetSpellHolderList()) {
            if (PlayerPrefs.GetInt(item.Type.ToString(), 0) == 1)
                item.IsChallangeDone = true;
            else
                item.IsChallangeDone = false;
        } 
    }
    public void RegisterChallangeDone(SpellType type) {
        foreach (var item in _spells.GetSpellHolderList()) {
            if(item.Type== type) { 
                item.IsChallangeDone= true;
                PlayerPrefs.SetInt(item.Type.ToString(), 1);
            }
        }
    }
}
