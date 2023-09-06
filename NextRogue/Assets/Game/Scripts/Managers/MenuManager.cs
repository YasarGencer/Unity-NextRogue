using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] PlayerList _playerStatList;
    [SerializeField] List<LoaderElement> _loaderSettings;
    public PlayerList PlayerStatList { get { return _playerStatList; } }
    [Header("PANELS")]
    [SerializeField] MenuPanel_Start _startPanel;
    [SerializeField] MenuPanel_Play _playPanel;
    [SerializeField] MenuPanel_Options _optionsPanel;
    [SerializeField] LoadingManager _loadingPanel;
    CurrentPanel _currentPanel;
    enum CurrentPanel {
        START,
        PLAY,
        OPTIONS
    }

    private void Awake() {
        _currentPanel = CurrentPanel.START;

        _startPanel.Initialize(Play, Options, Quit);
        _playPanel.Initialize(_playerStatList, Back, Loading);
        _optionsPanel.Initialize(Back);
        _loadingPanel.Initialize();
        _startPanel.Open();
        _playPanel.gameObject.SetActive(false);

        AudioManager.CreateAudioManager();

    }
    void Play() {
        _currentPanel = CurrentPanel.PLAY;

        _startPanel.Close();
        _playPanel.Open(); 
    }
    void Options() {
        _currentPanel = CurrentPanel.OPTIONS;

        _startPanel.Close();
        _optionsPanel.Open(); 
    }
    void Loading()
    {
        _playPanel.Close();

        _loadingPanel.Open(_loaderSettings[GameManager.GetPlayerIndex()], () => { SceneManager.LoadScene(1); });
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
                _optionsPanel.Close();
                break;
            default:
                break;
        }
    }
    void Quit() => Application.Quit(); 
}
