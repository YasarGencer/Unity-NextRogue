using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Canvas_Pause_Screen : AUI {
    [SerializeField] Button[] _buttons;
    public override void Initialize() {
        base.Initialize();

        _buttons[0].onClick.AddListener(MainMenu);
        _buttons[0].transform.parent.GetComponent<TextMeshProUGUI>().SetText("mainmenu");
        _buttons[1].onClick.AddListener(Restart);
        _buttons[1].transform.parent.GetComponent<TextMeshProUGUI>().SetText("restart");
        _buttons[2].onClick.AddListener(Unpause);
        _buttons[2].transform.parent.GetComponent<TextMeshProUGUI>().SetText("unpause");

        Close();
    }
    public override void Open() {
        base.Open();
    }
    public override void Close(float time = 0) {
        base.Close();
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
}
