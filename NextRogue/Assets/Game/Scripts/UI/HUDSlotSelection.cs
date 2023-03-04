using TMPro;
using Unity.VisualScripting;
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
        _text.SetText(MainManager.Instance.Player.GetComponentInChildren<P_MainController>().Input.GetSkillKey(_keyIndex + 4));
        GetComponent<Button>().onClick.AddListener(OnClick);
    }
    void Init(bool value) {
        //GetComponent<Button>().interactable = !value;
        _text.gameObject.SetActive(!value);
        _icon.gameObject.SetActive(value);
    }
    void OnClick() {
        MainManager.Instance.CanvasManager.SkillSelection.SaveButton(_keyIndex);
    }
}
