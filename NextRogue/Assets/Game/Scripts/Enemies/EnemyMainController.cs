using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class EnemyMainController : MonoBehaviour
{

    [HideInInspector] public Rigidbody2D Rb;
    [HideInInspector] public Animator Animator;

    [HideInInspector]
    public EnemyStateHandler State;
    [HideInInspector]
    public EnemyStats Stats;
    [HideInInspector]
    public Health Health;
    [HideInInspector]
    public Damager Damager;

    private void Awake() => Initialize();
    public void Initialize() {

        Rb = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();

        State = GetComponent<EnemyStateHandler>();
        Stats = GetComponent<EnemyStats>();
        Health = GetComponent<Health>();
        Damager = GetComponent<Damager>();

        State.Initialize(this);
        Stats.Initialize();
        Health.Initialize();
        Damager.Initialize(Stats.Damage);
    }
}
