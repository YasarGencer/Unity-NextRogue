using System;
using System.Collections.Generic;
using UniRx; 
using UnityEngine;


public abstract class ANP_Use_Skill : MonoBehaviour {
    [SerializeField] List<ANP_Spell> _spell;

    protected IDisposable _updateRX;
    ANP_MainController _mainController;

    [SerializeField] float _firstSpellWaitTimer = 5f;
    //for last used spell
    public bool IsUsingSpell { get; private set; }
    public float WaitTimeAfterUseForOtherSpells { get ; private set; }
    public float WaitTimeAfterUse { get ; private set; }
    public bool CanMoveWhileWait { get ; private set; }
    public bool CanNormalAttackWhileWait { get ; private set; }
    float _cureentTime = 0f;
    float _cureentTimeForOtherSpells = 0f;


    public virtual void Initialize(ANP_MainController mainController) {
        _mainController = mainController;
        IsUsingSpell = false;

        for (int i = 0; i < _spell.Count; i++) {
            _spell[i] = Instantiate(_spell[i]);
            GetSpell(i).IsInit = false;
        }
        RegisterEvents();
        _updateRX?.Dispose();
        _updateRX = Observable.EveryUpdate().TakeUntilDisable(this).Subscribe(UpdateRX); 

    }
    private void OnDestroy() {
        UnRegisterEvents();
        _updateRX?.Dispose();
    }
    protected virtual void UpdateRX(long obj) {
        if (_firstSpellWaitTimer > 0f) {
            _firstSpellWaitTimer -= Time.deltaTime;
            return;
        }
        if (IsUsingSpell) {
            if(_cureentTime <= 0f) {
                IsUsingSpell = false;
                return;
            }
            _cureentTime-= Time.deltaTime;
            return;
        }
        if(_cureentTimeForOtherSpells > 0)
            _cureentTimeForOtherSpells -= Time.deltaTime;
        else {
            for (int i = 0; i < _spell.Count; i++) {
                if (CheckConditions(i)) {
                    PlaySpell(i);
                    return;
                }
            }
        }
    }
    protected virtual ANP_Spell GetSpell(int index) {
        return _spell[index];
    }
    protected virtual void PlaySpell(int index) {
        GetSpell(index).Initialize(_mainController);
        SetRestirectionDataFromSpell(GetSpell(index));
        _mainController.Animator.SetTrigger(index.ToString());
    }
    protected bool CheckConditions(int index) {
        return GetSpell(index).CheckConditions(_mainController);
    }
    public void SetRestirectionDataFromSpell(ANP_Spell spell) {
        IsUsingSpell = true;
        WaitTimeAfterUseForOtherSpells = spell.WaitTimeAfterUseForOtherSpells;
        WaitTimeAfterUse = spell.WaitTimeAfterUse;
        WaitTimeAfterUse = spell.WaitTimeAfterUse;
        CanMoveWhileWait = spell.CanMoveWhileWait;
        CanNormalAttackWhileWait = spell.CanNormalAttackWhileWait;
        _cureentTime = WaitTimeAfterUse;
        _cureentTimeForOtherSpells = WaitTimeAfterUseForOtherSpells;
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
    protected virtual void OnGamePause() {
        _updateRX?.Dispose();
    }
    protected virtual void OnGameUnPause() {
        _updateRX?.Dispose();
        _updateRX = Observable.EveryUpdate().TakeUntilDisable(this).Subscribe(UpdateRX);
    }
}
