using System.Collections;
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
    [SerializeField]
    NP_Stats _stats;
    public NP_Stats Stats { get; private set; }

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

        Health = GetComponent<Health>();
        Target = GetComponent<ANP_Target>();
        Movement = GetComponent<ANP_Movement>();
        Attack = GetComponent<ANP_Attack>();
        Stats = Instantiate(_stats);

        Stats.Initialize();
        Health.Initialize();
        Target.Initialize(this);
        Movement.Initialize(this);
        Attack.Initialize(this);

        if (this.CompareTag("Summoned"))
            Invoke("EndSummonLife", Stats.LifeSpan);

    }
    //COMMON FUNCTIONS
    public float Distance(Transform target) {
        return Vector2.Distance(transform.position, target.position);
    }
    public void Die() {
        Movement.Die();
        Attack.Die();

        Destroy(gameObject);
    }
    void EndSummonLife() {
        Health.GetDamage(10000,this.transform);
    }
}
