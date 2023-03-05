using System.Collections;
using System.Collections.Generic;
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

        Instantiate(_playerList.GetPlayer(PlayerPrefs.GetInt("Player", 0)).Player, MainManager.Instance.Player);

    }
    void OnGamePause() {
        _gamePaused = true;
    }
    void OnGameUnPause() {
        _gamePaused = false;
    }
}
