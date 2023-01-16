using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class NonPlayerMainController : MonoBehaviour
{
    [HideInInspector] public GameObject Player;


    [HideInInspector] public GameObject Target;

    [HideInInspector]
    public Animator Animator;
    [HideInInspector]
    public Rigidbody2D Rb;

    [HideInInspector]
    public Health Health;
    [HideInInspector]
    public NonPlayerStats Stats;
    [HideInInspector]
    public NonPlayerStateHandler State;

    [HideInInspector]
    public bool CanAttack = false;

    public void Awake() {
        Invoke("Initialize", 1f);
    }
    public void Initialize() {

        Player = GameObject.FindGameObjectWithTag("Player");

        Rb = Rb == null ? GetComponent<Rigidbody2D>() : Rb;
        Animator = Animator == null ? GetComponent<Animator>() : Animator;

        Stats = GetComponent<NonPlayerStats>();
        Health = GetComponent<Health>();
        State = GetComponent<NonPlayerStateHandler>();

        Stats.Initialize();
        State.Initialize(this);
        Health.Initialize();

        CanAttack = true;
    }
    public void ChangeTarget(GameObject target) {
        Target = target;
    }
    public void Attack() {
        CanAttack = false;
        Animator.SetTrigger("attack");
        Invoke("Hit", .5f);
        Invoke("SetAttack", .5f);
    }
    void Hit() {
        if(Target && Vector2.Distance(transform.position,Target.transform.position) < Stats.Range + 1)
            Target.GetComponent<Health>().GetDamage(Stats.Damage, transform);
    }
    void SetAttack() {
        CanAttack = true;
    }
}
