using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDSkillIcon : MonoBehaviour {
    PlayerMainController _mainController;
    ASpell _spell;

    [SerializeField] Slider _slider;
    [SerializeField] TextMeshProUGUI _name;
    [SerializeField] TextMeshProUGUI _key;
    [SerializeField] Image[] _icon;
    public Slider Slider { get { return _slider; } }
    public void Initialize(ASpell asell, PlayerMainController mainController) {
        _spell= asell;
        _mainController = mainController;
        SetIcon();
    }
    public void SetIcon() {
        foreach (var item in _icon) {
            item.sprite = _spell.Icon;
        }
        _name.text = _spell.Name;
    }
    public void SetKey(string key) {
        _key.text = key;
    }
    public void TriggerEnter() {
        _mainController.UI.SetSpellDescription(_spell);
    }
    public void TriggerExit() {
        _mainController.UI.CloseSpellDescription();
    }
}
