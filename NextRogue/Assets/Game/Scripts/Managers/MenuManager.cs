using UnityEngine; 
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] PlayerList _playerStatList;
    public PlayerList PlayerStatList { get { return _playerStatList; } }
    [Header("PANELS")]
    [SerializeField] MenuPanel_Start _startPanel;
    [SerializeField] MenuPanel_Play _playPanel;

    CurrentPanel _currentPanel;
    enum CurrentPanel {
        START,
        PLAY,
        OPTIONS
    }

    private void Awake() {
        _currentPanel = CurrentPanel.START;

        _startPanel.Initialize(Play, Options, Quit);
        _playPanel.Initialize(_playerStatList, Back);
        _startPanel.Open();
        _playPanel.gameObject.SetActive(false);
    }
    void Play() {
        _currentPanel = CurrentPanel.PLAY;

        _startPanel.Close();
        _playPanel.Open(); 
    }
    void Options() {
        _currentPanel = CurrentPanel.OPTIONS;

        _startPanel.Close();
        //_optionsPanel.gameObject.SetActive(true); 
    }
    void Back() {
        switch (_currentPanel) {
            case CurrentPanel.START:
                break;
            case CurrentPanel.PLAY:
                _playPanel.Close();
                _startPanel.Open();
                break;
            case CurrentPanel.OPTIONS: 
                _startPanel.Open();
                break;
            default:
                break;
        }
    }
    void Quit() => Application.Quit(); 
}
