using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDKeyBinding : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _keyName;
    [SerializeField] TextMeshProUGUI _keyBind;
    [SerializeField] Button _startToListenButton;
    [SerializeField] Button _revertButton;

    public void Initialize(string name, string bind) {
        _keyName.SetText(name); 
        _keyBind.SetText(bind); 
    } 
}
