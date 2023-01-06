using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class PlayerBasicAttacks : MonoBehaviour {
    private bool _isInit = false;


    IDisposable cooldownRX;
    [SerializeField] float _cooldownTime;
    [SerializeField] float _activeTimer;

    [Header("MELEE")]
    [SerializeField] GameObject _mProjectile;
    [SerializeField] float _mDamage;

    [Header("RANGED")]
    [SerializeField] GameObject _rProjectile;
    [SerializeField] float _rDamage;

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
    GameObject projectile;
    void Melee() {
        projectile = Instantiate(_mProjectile, this.transform.position, Quaternion.identity);
        projectile.GetComponentInChildren<PlayerBasicMeleeProjectile>().Initialize(_inputManager.MousePosition);
    }
    void Ranged() {
        projectile = Instantiate(_rProjectile, this.transform.position, Quaternion.identity);
        projectile.GetComponentInChildren<PlayerBasicRangedProjectile>().Initialize(_inputManager.MousePosition);
    }
    void Common() {
        _animator.SetTrigger("basic");

        _activeTimer = _cooldownTime;

        cooldownRX?.Dispose();
        cooldownRX = Observable.EveryUpdate().TakeUntilDisable(this).Subscribe(Cooldown);
    }
    void Cooldown(long obj) {
        if (_activeTimer <= 0)
            cooldownRX?.Dispose();
        _activeTimer -= 1 * Time.deltaTime;
    }
}
