using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro; 
using DG.Tweening;

public class Canvas_Pause_Screen : AUI {
    [SerializeField] Button[] _buttons;
    [SerializeField] VerticalLayoutGroup _buttonsParent;

    [SerializeField] AUI _optionsPanel, _controlPanel, _mapPanel;

    float _buttonYScale; 
    public override void Initialize() {
        base.Initialize();

        _optionsPanel.Initialize();
        _controlPanel.Initialize();
        _mapPanel.Initialize();

        _buttonsParent.childControlHeight = false;
        _buttonYScale = _buttons[0].transform.localScale.y; 

        _buttons[0].onClick.AddListener(MainMenu);
        _buttons[1].onClick.AddListener(delegate { OpenAsPage(_controlPanel, 1); _optionsPanel.Close(.5f); }); 
        _buttons[2].onClick.AddListener(delegate { OpenAsPage(_optionsPanel, 2); _controlPanel.Close(.5f); });
        _buttons[3].onClick.AddListener(Unpause); 

        Close();
    }
    public override void Open() {
        base.Open();
    }
    public override void Close(float time = 0) {
        base.Close(time);
    }

    void MainMenu() {
        SceneManager.LoadScene(0);
    }
    void Restart() { 
        SceneManager.LoadScene(1);
    }
    void Unpause() {
        MainManager.Instance.EventManager.RunOnGameUnPuase();
    } 
    void OpenAsPage(AUI panel,int index) { 
        panel.ButtonPressed();
        if (panel.isOpen) {
            if (_mapPanel.isOpen)
                _mapPanel.Close(.5f);
            for (int i = 0; i < _buttons.Length; i++)
                if (i != index) {
                    _buttons[i].transform.DOScaleY(_buttonYScale / 1.25f, 1);
                }
            _buttons[index].transform.DOScaleY(_buttonYScale * 1.25f, 1);
        } else {
            if (_mapPanel.isOpen == false)
                _mapPanel.Open();
            for (int i = 0; i < _buttons.Length; i++)
                if (i != index) {
                    _buttons[i].transform.DOScaleY(_buttonYScale, 1);
                }
            _buttons[index].transform.DOScaleY(_buttonYScale, 1);
        }
    }
}
