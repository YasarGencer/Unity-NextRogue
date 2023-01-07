using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateHandler : MonoBehaviour {
    [System.Serializable]
    public struct Enemy {
        public string Name;
        public float Speed;
        public float Damage;
        public float Health;
        [HideInInspector] public Rigidbody2D Rb;
        [HideInInspector] public Animator Animator;
    }

    [SerializeField] Enemy _enemy;
    [HideInInspector]
    public bool IsInit = false;

    [HideInInspector]
    public AEnemyStates _activeState;
    [HideInInspector]
    public Health _health;
    [HideInInspector]
    public Damager _damager;

    public Enemy GetEnemy() { return _enemy; }
    public void SetEnemy(EnemyStateHandler.Enemy enemy) { _enemy = enemy; }
    private void Awake() {
        Initialize();
        
    }
    public void Initialize() {
        _enemy.Rb = GetComponent<Rigidbody2D>();
        _enemy.Animator = GetComponent<Animator>();
        _health = GetComponent<Health>();
        _damager = GetComponent<Damager>();

        _health.Initialize();
        _damager.Initialize(_enemy.Damage);

        SelectState(new EnemyStateAttack());
        IsInit = true;
    }
    public void SelectState(AEnemyStates state) {
        if(_activeState != null)
            _activeState.DeactivateState();
        _activeState = state;
        _activeState.ActivateState(this, this.gameObject);
    }

}
