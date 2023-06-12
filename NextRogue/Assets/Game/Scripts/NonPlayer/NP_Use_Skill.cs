using System;
using UniRx;
using UnityEngine;

public class NP_Use_Skill : MonoBehaviour
{
    [SerializeField] ANP_Spell _spell;

    protected IDisposable _updateRX;
    NP_MainController _mainController;
    public void Initialize(NP_MainController mainController) {
        _mainController = mainController;
        _spell = Instantiate(_spell);
        _spell.IsInit = false; 
        _spell.MainController= mainController;
        RegisterEvents();
        _updateRX?.Dispose();
        _updateRX = Observable.EveryUpdate().TakeUntilDisable(this).Subscribe(UpdateRX);
    }
    private void OnDestroy() {
        UnRegisterEvents();
        _updateRX?.Dispose();
    }
    bool CheckConditions() {
        return _spell.CheckConditions();
    }  
    protected virtual void UpdateRX(long obj) { 
        if(CheckConditions()) {
            _spell.Initialize(_mainController);
        }
    }
    // EVENTS 
    void RegisterEvents() {
        MainManager.Instance.EventManager.onGamePause += OnGamePause;
        MainManager.Instance.EventManager.onGameUnPause += OnGameUnPause;
    }
    void UnRegisterEvents() {
        MainManager.Instance.EventManager.onGamePause -= OnGamePause;
        MainManager.Instance.EventManager.onGameUnPause -= OnGameUnPause;
    }
    protected void OnGamePause() {
        _updateRX?.Dispose();
    }
    protected void OnGameUnPause() { 
        _updateRX?.Dispose();
        _updateRX = Observable.EveryUpdate().TakeUntilDisable(this).Subscribe(UpdateRX);
    }
}
