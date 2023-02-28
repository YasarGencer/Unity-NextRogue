using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Canvas_Pause_Screen : AUI {
    [SerializeField] Button _mainmenuButton;
    [SerializeField] Button _restartButton;
    [SerializeField] Button _unpauseButton;
    public override void Initialize() {
        base.Initialize(); 
        _mainmenuButton.onClick.AddListener(MainMenu);
        _restartButton.onClick.AddListener(Restart);
        _unpauseButton.onClick.AddListener(Unpause);

        Close();
    }
    public override void Open() {
        base.Open();
    }
    public override void Close() {
        base.Close();
    }

    void MainMenu() {
        SceneManager.LoadScene(0);
    }
    void Restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    void Unpause() {
        MainManager.Instance.EventManager.RunOnGameUnPuase();
    }
}
