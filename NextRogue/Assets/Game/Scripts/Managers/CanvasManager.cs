using DG.Tweening;
using System;
using TMPro;
using UniRx;
using UnityEngine;

public class CanvasManager : AUI
{
    bool _isInit = false;
    [SerializeField]
    Canvas_Player_GUI_HUD _playerHud;
    [SerializeField]
    Canvas_Pause_Screen _pauseScreen;
    [SerializeField]
    Canvas_Skill_Selection _skillSelection; 
    public Canvas_Player_GUI_HUD Player_GUI_HUD { get { return _playerHud; } }
    public Canvas_Pause_Screen PauseScreen { get { return _pauseScreen; } }
    public Canvas_Skill_Selection SkillSelection { get { return _skillSelection; } } 

    [SerializeField] CanvasGroup _track;
    [SerializeField] TextMeshProUGUI _trackName, _trackLength;
    [SerializeField] TextMeshProUGUI _textPopUp;

    public override void Initialize() {
        RegisterEvents();
         
        if (!_isInit) {
            _playerHud.Initialize();
            _pauseScreen.Initialize();
            _skillSelection.Initialize(); 
            EventManager.onTrackStart += ShowTackInfo; 
        } 
        _track.DOFade(0, 0);

        _isInit = true;
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
    protected override void OnPlayerInitialized() {
        base.OnPlayerInitialized();

        OpenPlayerHud();
        /*
        if (MainManager.Instance.IsTest == false &&MainManager.Instance.LevelManager.GetLevel() == 0 && MainManager.Instance.LevelManager.ActiveLevelSetting.TutorialLevel.IsTutorial == false)
            OpenSkillSelection();
        else
            OpenPlayerHud();
        */
    }
    public void OpenSkillSelection(ShopItemSlot shopFront) {
        MainManager.Instance.EventManager.RunOnGamePause(); 
        _pauseScreen.Close(); 
        _skillSelection.Open(shopFront);
    }
    void OpenPlayerHud() {
        _playerHud.Open();
    }  
    void ShowTackInfo(AudioClip clip) { 
        var name = clip.name;
        name = name.Split('-')[1];
        _trackName.SetText(name);
        var duration = TimeSpan.FromSeconds(clip.length);
        float f = (float)(duration.Minutes + duration.Seconds / 100f); 
        var length = f.ToString().Split(',')[0] + ":" + f.ToString().Split(",")[1];
        _trackLength.SetText(length);

        _track.DOFade(0, 0);
        _track.DOFade(1, .5f);
        Observable.Timer(TimeSpan.FromSeconds(3f)).TakeUntilDisable(this).Subscribe(l => {
            _track.DOFade(0, 1);
        });
    }

    public void TextPopUp(bool value, string text = "") { 
        _textPopUp.gameObject.SetActive(value);
        _textPopUp.SetText(text);
    }
}
