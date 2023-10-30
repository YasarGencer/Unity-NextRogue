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
     
    float _spellTimer = 0f; 
    float _moveTimer = 0f; 
    float _attackTimer = 0f;

    public bool SpellTimer { get { return _spellTimer <= 0; } }
    public bool MoveTimer { get { return _moveTimer <= 0; } }
    public bool AttackTimer { get { return _attackTimer <= 0; } }
     


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
        if(_moveTimer > -1)
            _moveTimer -= Time.deltaTime;
        if(_attackTimer > -1)
            _attackTimer -= Time.deltaTime;
        if (_spellTimer > -1)
            _spellTimer -= Time.deltaTime;
        else {
            if (_mainController.Attack.IsAttacking)
                return;
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
        _spellTimer = spell.SpellTimer;
        _moveTimer = spell.MoveTimer;
        _attackTimer = spell.AttackTimer;  
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
