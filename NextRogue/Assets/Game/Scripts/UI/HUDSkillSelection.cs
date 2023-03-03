using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDSkillSelection : MonoBehaviour {
    [SerializeField]
    Image _icon;
    [SerializeField]
    TextMeshProUGUI _name, _descriptipon;
    bool isSpell;
    ASpell _spell; 
    public void Initialize(ASpell spell) {
        isSpell = true;
        _spell = spell;
        _icon.sprite = _spell.Icon; 
        _name.text = _spell.Name; 
        _descriptipon.text = _spell.Description; 
    }
    public void Save() {
        if(isSpell)
            MainManager.Instance.CanvasManager.SlikkSelection.SaveSelected(_spell);
    } 
}
