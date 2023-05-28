using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class MenuPanel_Options : AUI {
    [SerializeField] HUDSound HUDSound;
    [SerializeField] Button _backButton;
    public void Initialize(UnityAction back) {
        _child = transform.GetChild(0);
        _backButton.onClick.AddListener(back);
        Close();
    }
    public override void Open() {
        base.Open();
    }
    public override void Close(float time = 0) {
        base.Close();
    }  
}
