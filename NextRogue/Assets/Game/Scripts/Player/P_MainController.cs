using System.Collections;
using UnityEngine; 

public class P_MainController : MonoBehaviour {
    [HideInInspector]
    public bool canPlay = false;
    [HideInInspector]
    public Animator Animator { get; private set; }
    [HideInInspector]
    public Rigidbody2D Rb { get; private set; }

     
    public P_InputManager Input { get; private set; }
    [SerializeField]
    P_Stats _stats;
    [HideInInspector] 
    public P_Stats Stats;
    public Health Health { get; private set; } 
    public P_Movement Movement { get; private set; } 
    public P_SpellHandler Spells { get; private set; } 
    public P_LevelHandler Level { get; private set; } 
    public Canvas_Player_GUI_HUD UI { get; private set; }

    GameObject _child;  
    public void Initialize() => StartCoroutine(InitializeCoroutine());
    public IEnumerator InitializeCoroutine() { 

        transform.position = Vector3.zero;
        _child = transform.GetChild(0).gameObject;
        _child.SetActive(false);

        canPlay = false;

        Rb = Rb == null ? GetComponent<Rigidbody2D>() : Rb;
        Animator = Animator == null ? GetComponent<Animator>() : Animator;

        if(this.Input== null)
        this.Input = gameObject.AddComponent<P_InputManager>();
        if (this.Health == null)
            Health = GetComponent<Health>();
        if (this.Level == null)
            Level = GetComponent<P_LevelHandler>();
        if (this.Stats == null)
            Stats = Instantiate(_stats);
        if (this.Movement == null)
            Movement = GetComponent<P_Movement>();
        if (this.Spells == null)
            Spells = GetComponent<P_SpellHandler>();
        if (this.UI == null)
            UI = MainManager.Instance.CanvasManager.Player_GUI_HUD;
         
         

        yield return new WaitForSeconds(1.5f); 

        _child.SetActive(true);
        GetComponent<Animator>().SetTrigger("start");

        yield return new WaitForSeconds(.5f);

        canPlay = true;

        this.Input.Initialize(this);
        Level.Initialize(this);
        Movement.Initialize(this);
        Spells.Initialize(this);
        Health.Initialize();

        yield return new WaitForSeconds(.25f);

        MainManager.Instance.EventManager.PlayerInitialized();
        GameObject.FindObjectOfType<TutorialManager>()?.OnPlayerInitialized();
    }
 
}
