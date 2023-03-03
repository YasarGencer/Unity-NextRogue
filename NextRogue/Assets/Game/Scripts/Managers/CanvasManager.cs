using System;
using UnityEngine;

public class CanvasManager : AUI
{
    [SerializeField]
    Canvas_Player_GUI_HUD _playerHud;
    [SerializeField]
    Canvas_Pause_Screen _pauseScreen;
    [SerializeField]
    Canvas_Skill_Selection _skillSelection;
    public Canvas_Player_GUI_HUD Player_GUI_HUD { get { return _playerHud; } }
    public Canvas_Pause_Screen PauseScreen { get { return _pauseScreen; } }
    public Canvas_Skill_Selection SlikkSelection { get { return _skillSelection; } }

    public override void Initialize() {
        base.Initialize();
        _playerHud.Initialize();
        _pauseScreen.Initialize();
        _skillSelection.Initialize(); 
        Invoke("OpenSkillSelection", 2);
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
        _skillSelection.Close(); 
    }
    public void OpenSkillSelection() {
        MainManager.Instance.EventManager.RunOnGamePause();
        _pauseScreen.Close(); 
        _skillSelection.Open();
    }
    void OpenPlayerHud() {
        _playerHud.Open();
    }  
}
