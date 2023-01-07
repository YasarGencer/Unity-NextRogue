using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class PlayerBasicAttacks : MonoBehaviour {
    private bool _isInit = false;


    IDisposable cooldownRX;
    float _activeTimer;

    [Header("MELEE")]
    [SerializeField] GameObject _mProjectile;

    [Header("RANGED")]
    [SerializeField] GameObject _rProjectile;

    [SerializeField]bool _canAttack;
    public bool CanAttack { get { return _canAttack; } }
    int _value;


    InputManager _inputManager;
    Rigidbody2D _rb;
    Animator _animator;

    public void Initialize(InputManager inputManager) {
        _inputManager = inputManager;

        _rb = _inputManager.Rb;
        _animator = _inputManager.Animator;

        _canAttack = true;

        _isInit = true;
    }
    public void Basic(int value) {
        if (!_isInit || !_canAttack || _activeTimer > 0)
            return;

        _value = value;

        Common();

        if (_value == 1)
            Melee();
        else
            Ranged();
    }
    void Melee() => Instantiate(_mProjectile, this.transform.position, Quaternion.identity).GetComponent<PlayerBasicMeleeProjectile>().Initialize(_inputManager.GetMousePos(),_inputManager.PlayerStats.Basic1Damage);

    void Ranged() => Instantiate(_rProjectile, this.transform.position, Quaternion.identity).GetComponent<PlayerBasicRangedProjectile>().Initialize(_inputManager.GetMousePos(), _inputManager.PlayerStats.Basic2Damage);
    void Common() {
        _animator.SetTrigger("basic");

        _activeTimer = _inputManager.PlayerStats.BasicCooldown;

        cooldownRX?.Dispose();
        cooldownRX = Observable.EveryUpdate().TakeUntilDisable(this).Subscribe(Cooldown);
    }
    void Cooldown(long obj) {
        if (_activeTimer <= 0)
            cooldownRX?.Dispose();
        _activeTimer -= 1 * Time.deltaTime;
    }
}
