using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UniRx;
using System;

public abstract class ASpell : ScriptableObject
{
    protected PlayerMainController _mainController;
    public PlayerMainController MainController { get { return _mainController; } }

    int _index;
    int _keyIndex;
    bool _isInit;
    public int Index { get { return _index; } set { _index = value; } }
    public int KeyIndex { get { return _keyIndex; } set { _keyIndex = value; } }
    public bool IsInit { get { return _isInit; } set { _isInit = value; } }

    //public bool IsBasic;
    public string Name;
    [TextArea]
    public string Description;

    public Sprite Icon;

    public GameObject Spell;
    protected GameObject Instantiated;

    public float Damage;

    public float CastingTime;
    float _currentTimeCast;
    IDisposable _castRX;

    public float CooldownTime;
    float _currentTimeCooldown;
    IDisposable _cooldownRX;


    public virtual void Initialize(PlayerMainController mainController, int value) {
        if (_isInit && _currentTimeCooldown < CooldownTime)
            return;

        _keyIndex = value;
        _mainController = mainController;
        Instantiated = null;

        _currentTimeCast = CastingTime;
        _currentTimeCooldown= CooldownTime;

        if(CastingTime > 0)
            StartCasting();
        else
            ActivateSpell();
        _isInit = true;
    }
    public virtual void ActivateSpell() {
        if (_keyIndex <= 3)
            _mainController.Animator.SetTrigger("basic");
        else
            _mainController.Animator.SetTrigger("spell");
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

        _mainController.UI.SetSlider(
            _mainController.UI.spellIconList[_keyIndex].Slider,
            CooldownTime,
            CooldownTime - _currentTimeCooldown);
    }
    public void JustCast(Vector3 pos) {
        Instantiate(
            Spell,
            pos,
            Quaternion.identity
            ).GetComponent<APlayerProjectile>()
            .Initialize(_mainController.Input.GetMouseWolrdPos(),
            Damage, CooldownTime);
    }
}
