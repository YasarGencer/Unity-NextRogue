using UnityEngine;
public class P_MainController : MonoBehaviour {
    
    [HideInInspector]
    public Animator Animator;
    [HideInInspector]
    public Rigidbody2D Rb;


    [HideInInspector]
    public P_InputManager Input;
    [HideInInspector]
    public P_Stats Stats;
    [HideInInspector]
    public Health Health;
    [HideInInspector]
    public P_Movement Movement;
    [HideInInspector]
    public P_SpellHandler Spells;
    [HideInInspector]
    public MainGUIHUD UI;

    GameObject _child;

    public void Start() {
        if (MainGUIHUD.Instance)
            StartCutscene();
        else
            Initialize();
    }
    public void Initialize() {
        if (MainGUIHUD.Instance)
            MainGUIHUD.Instance.gameObject.SetActive(true);

        Rb = Rb == null ? GetComponent<Rigidbody2D>() : Rb;
        Animator = Animator == null ? GetComponent<Animator>() : Animator;

        this.Input = gameObject.AddComponent<P_InputManager>();
        Stats = GetComponent<P_Stats>();
        Health = GetComponent<Health>();
        Movement = GetComponent<P_Movement>();
        Spells = GetComponent<P_SpellHandler>();
        UI = MainGUIHUD.Instance;

        this.Input.Initialize(this);
        Stats.Initialize();
        if(UI)
            UI.Initialize(this);
        Movement.Initialize(this);
        Spells.Initialize(this);
        Stats.Initialize(); 
        Health.Initialize();
    }
    void StartCutscene() {
        _child = transform.GetChild(0).gameObject;
        _child.SetActive(false);
        MainGUIHUD.Instance.gameObject.SetActive(false);
        Invoke("StartCutsceneEnd", 1.5f);
    }
    void StartCutsceneEnd() {
        _child.SetActive(true);
        GetComponent<Animator>().SetTrigger("start");
        Invoke("Initialize", .5f);
    }
}
