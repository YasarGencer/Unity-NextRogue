using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class MenuPanel_Start : AUI {
    [Header("BUTTONS")]
    [SerializeField] Button _playButton;
    [SerializeField] Button _optionsButton, _quitButton;

    public void Initialize(UnityAction play, UnityAction options, UnityAction quit) {
        _child = transform.GetChild(0); 
        _playButton.onClick.AddListener(play);
        _optionsButton.onClick.AddListener(options);
        _quitButton.onClick.AddListener(quit);
    } 
    public override void Open() {
        base.Open();
    }
    public override void Close(float time = 0) {
        base.Close(time);
    }
}
