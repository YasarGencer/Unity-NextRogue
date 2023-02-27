using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDSkillIcon : MonoBehaviour {
    P_MainController _mainController;
    ASpell _spell;

    [SerializeField] Slider _slider;
    [SerializeField] TextMeshProUGUI _name;
    [SerializeField] TextMeshProUGUI _key;
    [SerializeField] Image[] _icon;
    public Slider Slider { get { return _slider; } }
    public void Initialize(ASpell asell, P_MainController mainController) {
        _spell= asell;
        _mainController = mainController;
        _slider.value = _slider.maxValue;
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
        if (_spell != null)
            _mainController.UI.Description.Show(_spell);
    }
    public void TriggerExit() {
        if (_spell != null)
            _mainController.UI.Description.Hide();
    }
}
