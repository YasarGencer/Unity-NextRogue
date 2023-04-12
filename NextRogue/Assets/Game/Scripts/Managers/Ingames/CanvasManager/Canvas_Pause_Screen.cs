using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Unity.VisualScripting;

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
    public override void Close() {
        base.Close();
    }

    void MainMenu() { 
        MainManager.Instance.GameManager.LoadScene(1, 2);
    }
    void Restart() {
        GameObject.FindObjectOfType<P_MainController>().Stats.ResetStats(); 
        MainManager.Instance.GameManager.LoadScene(2,2);
    }
    void Unpause() {
        MainManager.Instance.EventManager.RunOnGameUnPuase();
    }
}
