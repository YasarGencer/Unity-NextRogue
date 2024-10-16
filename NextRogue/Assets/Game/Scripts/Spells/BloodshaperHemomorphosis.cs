using System; 
using UnityEngine;
using UniRx;

[CreateAssetMenu(fileName = "BloodshaperHemomorphosis", menuName = "ScriptableObjects/Spells/BloodshaperHemomorphosis")]
public class BloodshaperHemomorphosis : ASpell {
    [SerializeField]
    float _speed, _timer;
    static bool isSubscribed = false;
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
        if (isSubscribed == false) {
            MainManager.Instance.EventManager.onSpellUsed += SpellUsed;
        }
    }
    void Hemomorphosis(bool value) {
        var color = _mainController.GetComponentInChildren<SpriteRenderer>().color;
        _mainController.Stats.IsInvincable = value;
        if (value) {
            //color.a = 0.3f;
            _speedHolder = _speed + _speedMultiplier * _mainController.Stats.SecondaryValue;
            _mainController.Stats.Speed += _speedHolder;
            _mainController.Health.GainHealth(_mainController.Stats.SecondaryValue * _healMultiplier);
            _mainController.Stats.SetSecondaryValue(0);
        } else {
            //color.a = 1f;
            _mainController.Stats.Speed -= _speedHolder;
            _mainController.Animator.SetTrigger("0Next");
        }
        _mainController.GetComponentInChildren<SpriteRenderer>().color = color;
    }
    void SpellUsed() {
        if (_time <= 0)
            return;
        StopRX();
        Hemomorphosis(false);
        MainManager.Instance.EventManager.onSpellUsed -= SpellUsed;
        isSubscribed = false;
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
        _time = 0;
        _spellRX?.Dispose();
    }
}
