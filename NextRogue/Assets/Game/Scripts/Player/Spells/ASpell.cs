using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UniRx;
using System;

public abstract class ASpell : ScriptableObject
{
    PlayerMainController _mainController;

    public int Index;
    public string Name;
    public string ShortDesc;
    public string Description;

    public Sprite Icon;

    public GameObject Spell;
    protected GameObject Instantiated;

    public float CastingTime;
    IDisposable _castRX;
    float _currentTime;
    bool isInit;
    public virtual void Initialize(PlayerMainController mainController) {
        if (isInit && _currentTime < CastingTime)
            return;
        Debug.Log("Init");
        _mainController = mainController;
        Instantiated = null;
        _currentTime = CastingTime;
        StartCasting();
        isInit = true;
    }
    public virtual void ActivateSpell() {
        _currentTime = CastingTime;
        Debug.Log(name);
    }
    public void StartCasting() { _castRX?.Dispose(); _castRX = Observable.EveryUpdate().TakeUntilDisable(_mainController).Subscribe(Timer); }
    public void StopCasting() { _castRX?.Dispose(); ActivateSpell(); }
    public void Timer(long obj) {
        if (_currentTime <= 0) {
            StopCasting();
            return;
        }
        _currentTime -= Time.deltaTime;
    }
}
