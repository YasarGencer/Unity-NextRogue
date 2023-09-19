using System.Collections; 
using UnityEngine; 

public class ANP_MainController : MonoBehaviour {
    public bool isTest;
    public P_MainController Player { get; private set; }
    public GameObject AttackTarget { get; private set; }

    [HideInInspector]
    public Animator Animator;
    [HideInInspector]
    public Rigidbody2D Rb;

    [HideInInspector]
    public Health Health;
    [SerializeField]
    NP_Stats _stats;
    public NP_Stats Stats { get; private set; }
     
    public ANP_Target Target { get; protected set; }  
    public ANP_Use_Skill UseSkill { get; protected set; }
    public ANP_Movement Movement { get; protected set; }
    public ANP_Attack Attack { get; protected set; }

    protected bool _isInit = false;
    private void Start() {
        Initialize(2f);
    }
    public void Initialize() {
        StartCoroutine(Init(0));
    }
    public void Initialize(float time) {
        StartCoroutine(Init(time));
    }
    IEnumerator Init(float time) {
        yield return new WaitForSeconds(time);
        if (!_isInit && !MainManager.Instance.GameManager.GamePaused) {
            Initializator();
        }
        yield return new WaitForEndOfFrame();
        if (!_isInit)
            StartCoroutine(Init(0));
    }
    protected virtual void Initializator() {
        _isInit = true;
        SetPlayer(MainManager.Instance.Player.GetChild(0).GetComponent<P_MainController>());
        Rb = Rb == null ? GetComponent<Rigidbody2D>() : Rb;
        Animator = Animator == null ? GetComponent<Animator>() : Animator;

        Health = GetComponent<Health>();
        Target = GetComponent<ANP_Target>();
        Stats = Instantiate(_stats);

        Stats.Initialize();
        Health.Initialize();
        Target.Initialize(this);
    }

    //COMMON FUNCTIONS
    public float Distance(Transform target) {
        return Vector2.Distance(transform.position, target.position);
    }
    public virtual void Die() {

        Destroy(gameObject);
    }
    void EndSummonLife() {
        Health.GetDamage(10000, this.transform);
    }
    public void SetPlayer(P_MainController player) {
        Player = player;
    }
}
