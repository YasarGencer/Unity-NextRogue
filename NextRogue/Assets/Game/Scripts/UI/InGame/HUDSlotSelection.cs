using TMPro; 
using UnityEngine;
using UnityEngine.UI;

public class HUDSlotSelection : MonoBehaviour
{
    [SerializeField]
    int _keyIndex;
    [SerializeField]
    TextMeshProUGUI _text;
    [SerializeField]
    Image _icon;
    public void Initialize(ASpell spell) {
        Init(true);
        _icon.sprite = spell.Icon;
    }
    public void Initialize() { 
        Init(false);
        _text.SetText(MainManager.Instance.InputManager.GetSkillKeys(_keyIndex + 4));

    }
    void Init(bool value) {
        //GetComponent<Button>().interactable = !value;
        _text.gameObject.SetActive(!value);
        _icon.gameObject.SetActive(value);
    }
    public void OnClick() { 
        MainManager.Instance.CanvasManager.SkillSelection.Buy(_keyIndex);
    }
}
