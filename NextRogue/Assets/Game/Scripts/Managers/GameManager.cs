using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    bool _gamePaused;
    public bool GamePaused { get { return _gamePaused; }}
    public void Initialize() { 
        MainManager.Instance.EventManager.onGamePause += OnGamePause;
        MainManager.Instance.EventManager.onGameUnPause += OnGameUnPause;
        MainManager.Instance.EventManager.RunOnGameUnPuase();
    }
    void OnGamePause() {
        _gamePaused = true;
    }
    void OnGameUnPause() {
        _gamePaused = false;
    }
}
