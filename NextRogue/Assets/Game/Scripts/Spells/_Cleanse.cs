using System;
using UniRx;
using UnityEngine;
using System.Collections.Generic;
using System.Data;

[CreateAssetMenu(fileName = "Cleanse", menuName = "ScriptableObjects/Spells/Cleanse")]
public class _Cleanse : ASpell { 
    [SerializeField] float _timer = 2;
    [SerializeField] float _speedBoost = .5f;
    float _time;
    IDisposable _updateRX;


    public int coolDownedSpellCounter = 0;
    public override void Initialize(P_MainController mainController, int value) {
        base.Initialize(mainController, value);
    }

    public override void ActivateSpell() {
        coolDownedSpellCounter = 0;
        base.ActivateSpell();
        Destroy(Instantiate(Spell, _mainController.transform), 2f);

        MainManager.Instance.Player.GetComponentInChildren<P_MainController>().Health.DOTReciever.ClearDOT();
        List<DOTInfo> list = MainManager.Instance.Player.GetComponentInChildren<P_MainController>().Health.DOTReciever.GetDOT();
        var damages = 0;
        foreach(var item in list) {
            damages += damages;
        }
        if (damages >= 10)
            MainManager.Instance.GameManager.ChallangeManager.RegisterChallangeDone(SpellType.Cleanse);

        _updateRX?.Dispose();
        _updateRX = Observable.EveryUpdate().TakeUntilDisable(_mainController).Subscribe(UpdateRX);
        _mainController.Stats.Speed += _speedBoost;
        _time = _timer;
    }
    void UpdateRX(long obj) {
        _time -= Time.deltaTime;
        if(_time <= 0) {
            _updateRX?.Dispose();
            _mainController.Stats.Speed -= _speedBoost;
        }
    }
    protected override void OnGamePause() {
        base.OnGamePause();
    }
    protected override void OnGameUnPause() {
        base.OnGameUnPause();
    }



}