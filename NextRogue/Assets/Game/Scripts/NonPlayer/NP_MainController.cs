using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class NP_MainController : MonoBehaviour
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
    public NP_Stats Stats;


    [HideInInspector]
    public ANP_Target Target;
    [HideInInspector]
    public ANP_Movement Movement;
    [HideInInspector]
    public ANP_Attack Attack;

    bool isInit = false;
    private void Start() {
        Initialize(2f);
    }
    public void Initialize(Room? room) {
        Init();
    }
    public void Initialize(float time) {
        StartCoroutine(InitializeOnTime(time));
    }
    IEnumerator InitializeOnTime(float time) {
        yield return new WaitForSeconds(time);
        Init();
    }
    void Init() {
        if (isInit)
            return;
        isInit = true;

        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<P_MainController>();

        Rb = Rb == null ? GetComponent<Rigidbody2D>() : Rb;
        Animator = Animator == null ? GetComponent<Animator>() : Animator;

        Stats = GetComponent<NP_Stats>();
        Health = GetComponent<Health>();
        Target = GetComponent<ANP_Target>();
        Movement = GetComponent<ANP_Movement>();
        Attack = GetComponent<ANP_Attack>();

        Stats.Initialize();
        Health.Initialize();
        Target.Initialize(this);
        Movement.Initialize(this);
        Attack.Initialize(this);

    }
    //COMMON FUNCTIONS
    public float Distance(Transform target) {
        return Vector2.Distance(transform.position, target.position);
    }
}