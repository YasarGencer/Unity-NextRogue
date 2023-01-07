using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class PlayerBasicAttacks : MonoBehaviour {

    IDisposable cooldownRX;
    float _activeTimer;

    [Header("MELEE")]
    [SerializeField] GameObject _mProjectile;

    [Header("RANGED")]
    [SerializeField] GameObject _rProjectile;

    bool _canAttack;
    public bool CanAttack { get { return _canAttack; } }
    int _value;


    PlayerMainController _mainController;
    Animator _animator;

    public void Initialize(PlayerMainController mainController) {
        _mainController= mainController;

        _animator = _mainController.Animator;

        _canAttack = true;
    }
    public void Basic(int value) {
        if (!_canAttack || _activeTimer > 0)
            return;

        _value = value;

        Common();

        if (_value == 1)
            Melee();
        else
            Ranged();
    }
    void Melee() => Instantiate(_mProjectile, this.transform.position, Quaternion.identity).GetComponent<PlayerBasicMeleeProjectile>().Initialize(_mainController.Input.GetMousePos(),_mainController.Stats.Basic1Damage);

    void Ranged() => Instantiate(_rProjectile, this.transform.position, Quaternion.identity).GetComponent<PlayerBasicRangedProjectile>().Initialize(_mainController.Input.GetMousePos(), _mainController.Stats.Basic2Damage);
    void Common() {
        _animator.SetTrigger("basic");

        _activeTimer = _mainController.Stats.BasicCooldown;

        cooldownRX?.Dispose();
        cooldownRX = Observable.EveryUpdate().TakeUntilDisable(this).Subscribe(Cooldown);
    }
    void Cooldown(long obj) {
        if (_activeTimer <= 0)
            cooldownRX?.Dispose();
        _activeTimer -= 1 * Time.deltaTime;
    }
}
