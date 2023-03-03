using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    bool _gamePaused;
    public bool GamePaused { get { return _gamePaused; }}

    [SerializeField] AllSpells _spellList;
    public AllSpells AllSpells { get { return _spellList; }}
    public void Initialize() { 
        MainManager.Instance.EventManager.onGamePause += OnGamePause;
        MainManager.Instance.EventManager.onGameUnPause += OnGameUnPause;
        MainManager.Instance.EventManager.RunOnGameUnPuase();

        _spellList.Initialize();
    }
    void OnGamePause() {
        _gamePaused = true;
    }
    void OnGameUnPause() {
        _gamePaused = false;
    }
}
