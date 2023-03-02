using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class P_MainController : MonoBehaviour {
    
    [HideInInspector]
    public Animator Animator { get; private set; }
    [HideInInspector]
    public Rigidbody2D Rb { get; private set; }

     
    public P_InputManager Input { get; private set; }
    [SerializeField]
    P_Stats _stats;
    [HideInInspector] public P_Stats Stats;
    public Health Health { get; private set; } 
    public P_Movement Movement { get; private set; } 
    public P_SpellHandler Spells { get; private set; } 
    public P_LevelHandler Level { get; private set; } 
    public Canvas_Player_GUI_HUD UI { get; private set; }

    GameObject _child; 
    public void Start() {
        if (MainManager.Instance.CanvasManager.Player_GUI_HUD)
            StartCutscene();
        else
            Initialize();
    }
    public void Initialize() {
        InitializeFirstPart();
        InitializeSecondPart();
    }
    public void InitializeFirstPart() {
        Rb = Rb == null ? GetComponent<Rigidbody2D>() : Rb;
        Animator = Animator == null ? GetComponent<Animator>() : Animator;

        this.Input = gameObject.AddComponent<P_InputManager>();
        Health = GetComponent<Health>();
        Level = GetComponent<P_LevelHandler>();
        Stats = Instantiate(_stats);
        Movement = GetComponent<P_Movement>();
        Spells = GetComponent<P_SpellHandler>();
        UI = MainManager.Instance.CanvasManager.Player_GUI_HUD;
    }
    public void InitializeSecondPart() {
        this.Input.Initialize(this);
        Stats.Initialize();
        Level.Initialize(this);
        Movement.Initialize(this);
        Spells.Initialize(this);
        Health.Initialize();
    }
    void StartCutscene() {
        InitializeFirstPart();
        _child = transform.GetChild(0).gameObject;
        _child.SetActive(false);
        Invoke("StartCutsceneEnd", 1.5f);
    }
    void StartCutsceneEnd() {
        _child.SetActive(true);
        GetComponent<Animator>().SetTrigger("start");
        Invoke("InitializeSecondPart", .5f);
    }
}
