using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class Canvas_Pause_Screen : AUI {
    [SerializeField] AudioClip _clickClip;
    [SerializeField] Button[] _buttons;
    [SerializeField] VerticalLayoutGroup _buttonsParent;

    [SerializeField] AUI _optionsPanel, _controlPanel, _mapPanel;

    float _buttonYScale; 
    public override void Initialize() {
        base.Initialize();

        _optionsPanel.Initialize();
        _controlPanel.Initialize();
        _mapPanel.Initialize();

        _buttonYScale = _buttons[0].transform.localScale.y; 

        _buttons[0].onClick.AddListener(delegate { Click(); MainMenu(); });
        _buttons[1].onClick.AddListener(delegate { Click(); OpenAsPage(_controlPanel, 1); _optionsPanel.Close(.5f); }); 
        _buttons[2].onClick.AddListener(delegate { Click(); OpenAsPage(_optionsPanel, 2); _controlPanel.Close(.5f); });
        _buttons[3].onClick.AddListener(delegate { Click(); Unpause(); }); 

        Close();
    }
    public override void Open() {
        base.Open();

        _controlPanel.DOKill();
        _optionsPanel.DOKill();
        _mapPanel.DOKill();

        _controlPanel.Close();
        _optionsPanel.Close();
        _mapPanel.Open();

        foreach (var item in _buttons) {
            item.transform.DOKill();
            item.transform.DOScaleY(_buttonYScale, 0);
        }
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
        _buttonsParent.childControlHeight = false;
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
    void Click() {
        if (_clickClip) {
            AudioManager.PlaySound(_clickClip, null, AudioManager.AudioVolume.ui, false);
        }
    }
}
