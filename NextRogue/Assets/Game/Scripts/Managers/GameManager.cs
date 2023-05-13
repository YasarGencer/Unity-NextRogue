using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class GameManager : MonoBehaviour {
    bool _gamePaused;
    public bool GamePaused { get { return _gamePaused; } set { _gamePaused = value; } }

    [SerializeField] AllSpells _spellList;
    [SerializeField] PlayerList _playerList;
    public AllSpells AllSpells { get { return _spellList; }}
    public PlayerList PlayerList { get { return _playerList; }}
    public void Initialize() { 
        MainManager.Instance.EventManager.onGamePause += OnGamePause;
        MainManager.Instance.EventManager.onGameUnPause += OnGameUnPause;

        _spellList.Initialize();

        foreach (var item in _playerList.GetList())
            item.Stat.Initialize();

        Instantiate(_playerList.GetPlayer(GetPlayerIndex()).Player, MainManager.Instance.Player);

        if (_playerList.GetPlayer(GetPlayerIndex()).Stat.GetTutorial() == false)
            LevelSettings.SetIfTutorial(GetPlayerIndex());
    }
    void OnGamePause() {
        _gamePaused = true;
    }
    void OnGameUnPause() {
        _gamePaused = false;
    }
    public static int GetPlayerIndex() {
        return PlayerPrefs.GetInt("Player", 0);
    }
    public static void SetPlayerIndex(int value) {
        PlayerPrefs.SetInt("Player", value);
    }
}
