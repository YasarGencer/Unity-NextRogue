using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class NonPlayerMainController : MonoBehaviour
{
    public P_MainController Player { get; private set; }
    public GameObject AttackTarget { get; private set; }
    public Vector2 PatrolTarget { get; private set; }

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

    bool isInit = false;
    public void Initialize(Room room) {
        Init();
        State.Initialize(this, room);
    }
    public void Initialize() {
        Init();
        State.Initialize(this, null);
    }
    public void Initialize(float time) {
        StartCoroutine(InitOnTime(time));
    }
    IEnumerator InitOnTime(float time) {
        yield return new WaitForSeconds(time);
        Init();
        State.Initialize(this, null);
    }
    void Init() {
        if (isInit)
            return;
        isInit = true;

        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<P_MainController>();

        Rb = Rb == null ? GetComponent<Rigidbody2D>() : Rb;
        Animator = Animator == null ? GetComponent<Animator>() : Animator;

        Stats = GetComponent<NonPlayerStats>();
        Health = GetComponent<Health>();
        State = GetComponent<NonPlayerStateHandler>();

        Stats.Initialize();
        Health.Initialize();

        CanAttack = true;
    }
    public float CheckDistance(Vector2 pos) {
        return Vector2.Distance(transform.position, pos);
    }
    public void ChangeTarget(GameObject target) {
        AttackTarget = target;
    }
    public void ChangeTarget(Vector2 target) {
        PatrolTarget = target;
        AttackTarget = null;
    }
}
