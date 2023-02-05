using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class NonPlayerMainController : MonoBehaviour
{
    public P_MainController Player { get; private set; }
    public GameObject Target { get; private set; }

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

    float timer;
    bool isInit;

    public void Awake() {
        Invoke("Initialize", 1f);
    }
    public void Initialize() {

        timer = 0;
        isInit= true;

        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<P_MainController>();

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
        Invoke("SetAttack", Stats.AttackSpeed);
    }
    void Hit() {
        Health health = null;
        if (Target && Vector2.Distance(transform.position, Target.transform.position) < Stats.Range + 1)
            Target.TryGetComponent<Health>(out health);
        if(health != null)
            health.GetDamage(Stats.Damage, transform);
    }
    void SetAttack() {
        CanAttack = true;
    }
    private void Update() {
        if (!isInit || !this.CompareTag("Summoned"))
            return;
        timer += Time.deltaTime;
        if(timer > Stats.LifeSpan)
            Health.Die();
    }
}
