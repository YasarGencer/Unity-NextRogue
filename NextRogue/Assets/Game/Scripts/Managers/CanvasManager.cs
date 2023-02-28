using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CanvasManager : AUI
{
    [SerializeField]
    Canvas_Player_GUI_HUD _playerHud;
    [SerializeField]
    Canvas_Pause_Screen _pauseScreen;
    public Canvas_Player_GUI_HUD Player_GUI_HUD { get { return _playerHud; } }
    public Canvas_Pause_Screen PauseScreen { get { return _pauseScreen; } }

    public override void Initialize() {
        base.Initialize();
        _playerHud.Initialize();
        _pauseScreen.Initialize();
        Invoke("OpenPlayerHud", 2);
    }
    protected override void OnGamePause() {
        base.OnGamePause();
        _playerHud.Close();
        _pauseScreen.Open();
    }
    protected override void OnGameUnPause() {
        base.OnGameUnPause();
        _playerHud.Open();
        _pauseScreen.Close();
    }
    void OpenPlayerHud() {
        _playerHud.Open();
    }
}
