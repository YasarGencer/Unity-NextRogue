using UnityEngine;
using UniRx;
using System;
using TMPro;

public abstract class ASpell : ScriptableObject
{
    protected P_MainController _mainController;
    public P_MainController MainController { get { return _mainController; } }

    int _keyIndex;
    [SerializeField]
    bool _isInit = false;
    bool _isChoosen = false;
    public int KeyIndex { get { return _keyIndex; } set { _keyIndex = value; } }
    public bool IsInit { get { return _isInit; } set { _isInit = value; } }
    public bool IsChoosen { get { return _isChoosen; } set { _isChoosen = value; } }

    

    //public bool IsBasic;
    public string Name;
    [TextArea]
    public string Description; 

    public Sprite Icon;
    public Sprite ShopIcon;

    public AudioClip Sound;

    public GameObject Spell;
    protected GameObject Instantiated;

    public float Damage;
    public float Speed; 
    public DOTInfo DOTInfo;

    public float CastingTime;
    float _currentTimeCast;
    IDisposable _castRX;

    public float CooldownTime;
    protected float _currentTimeCooldown;
    IDisposable _cooldownRX;

    [SerializeField] protected bool stopAudio; 

    public virtual void Initialize(P_MainController mainController, int value) { 
        if (_isInit && _currentTimeCooldown < CooldownTime)
            return; 
        RegisterEvents();

        _keyIndex = value;
        _mainController = mainController;
        Instantiated = null;

        _currentTimeCast = CastingTime;
        _currentTimeCooldown= CooldownTime;

        MainManager.Instance.EventManager.RunOnSpellUsed();

        if (CastingTime > 0)
            StartCasting();
        else
            ActivateSpell();
        _isInit = true;
    }
    public virtual void ActivateSpell() {
        if (_keyIndex <= 3)
            _mainController.Animator.SetTrigger(_keyIndex.ToString());
        else
            _mainController.Animator.SetTrigger("4"); 

        if(Sound)
            AudioManager.PlaySound(Sound, _mainController.transform, AudioManager.AudioVolume.sfx, stopAudio);
        StartCooldown();
    }
    public void StartCasting() { _castRX?.Dispose(); _castRX = Observable.EveryUpdate().TakeUntilDisable(_mainController).Subscribe(CastingTimer); }
    public void StopCasting() { _castRX?.Dispose(); ActivateSpell(); _currentTimeCast = CastingTime; }
    public void StartCooldown() { _cooldownRX?.Dispose(); _cooldownRX = Observable.EveryUpdate().TakeUntilDisable(_mainController).Subscribe(CooldownTimer); }
    public void StopCooldown() { _cooldownRX?.Dispose(); _currentTimeCooldown = CooldownTime; }
    public void CastingTimer(long obj) {
        if (_currentTimeCast <= 0) {
            StopCasting();
            return;
        }
        _currentTimeCast -= Time.deltaTime;
    }
    public void CooldownTimer(long obj) {
        if (_currentTimeCooldown <= 0) {
            StopCooldown();
            return;
        }
        _currentTimeCooldown -= Time.deltaTime;

        if(_mainController.UI)
            _mainController.UI.SetSlider(
                _mainController.UI.spellIconList[_keyIndex].Slider,
                CooldownTime,
                CooldownTime - _currentTimeCooldown);
    }
    public GameObject JustCast(Vector3 pos) {
        var projectile = Instantiate(
            Spell,
            pos,
            Quaternion.identity
            );
        projectile.GetComponent<AProjectile>()
            .Initialize(MainManager.Instance.InputManager.GetMouseWorldPos(),
            Damage, CooldownTime, Speed, DOTInfo);
        return projectile;
    }
    public void RetrieveCooldown() {
        StopCooldown();  
        _mainController?.UI?.SetSlider(
            _mainController.UI.spellIconList[_keyIndex].Slider,
            1,
            1);
    }

    //EVENTS
    protected virtual void RegisterEvents() { 
        MainManager.Instance.EventManager.onGamePause += OnGamePause;   
        MainManager.Instance.EventManager.onGameUnPause += OnGameUnPause; 
    }
    protected virtual void OnGamePause() {
        _cooldownRX?.Dispose(); 
    }
    protected virtual void OnGameUnPause() {
        StartCooldown();
    } 
}
