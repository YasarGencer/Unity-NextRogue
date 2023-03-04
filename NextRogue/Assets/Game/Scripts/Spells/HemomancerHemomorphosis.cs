using System; 
using UnityEngine;
using UniRx;

[CreateAssetMenu(fileName = "HemomancerHemomorphosis", menuName = "ScriptableObjects/Spells/HemomancerHemomorphosis")]
public class HemomancerHemomorphosis : ASpell {
    [SerializeField]
    float _speed, _timer; 
    [SerializeField]
    float _speedMultiplier, _timerMultiplier, _healMultiplier;
    float _speedHolder, _time;
    IDisposable _spellRX;
    public override void Initialize(P_MainController mainController, int value) {
        base.Initialize(mainController, value);
    }
    public override void ActivateSpell() {
        base.ActivateSpell();
        _time = _timer + _timerMultiplier * _mainController.Stats.SecondaryValue;
        Hemomorphosis(true);
        StartRX();
    }
    void Hemomorphosis(bool value) {
        var color = _mainController.GetComponentInChildren<SpriteRenderer>().color;
        _mainController.Stats.IsInvincable = value;
        if (value) {
            color.a = 0.3f;
            _speedHolder = _mainController.Stats.Speed;
            _mainController.Stats.Speed += _speed + _speedMultiplier * _mainController.Stats.SecondaryValue;
            _mainController.Health.GainHealth(_mainController.Stats.SecondaryValue * _healMultiplier);
            _mainController.Stats.SetSecondaryValue(0);
        } else {
            color.a = 1f;
            _mainController.Stats.Speed = _speedHolder;
        }
        _mainController.GetComponentInChildren<SpriteRenderer>().color = color;
    }
    void SpellRX(long obj) {
        if(_time <= 0) {
            Hemomorphosis(false);
            StopRX();
            return;
        }
        _time -= Time.deltaTime;
    }
    void StartRX() {
        _spellRX?.Dispose();
        _spellRX = Observable.EveryUpdate().TakeUntilDisable(_mainController).Subscribe(SpellRX);
    }
    void StopRX() {
        _spellRX?.Dispose();
    }
}
