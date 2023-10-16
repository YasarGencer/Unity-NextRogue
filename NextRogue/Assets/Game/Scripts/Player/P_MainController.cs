using System.Collections;
using UnityEditor.TextCore.Text;
using UnityEngine;
using UnityEngine.Rendering;

public class P_MainController : MonoBehaviour {
    public bool canPlay { get; private set;}
    [HideInInspector]
    public Animator Animator { get; private set; }
    [HideInInspector]
    public Rigidbody2D Rb { get; private set; }

    [SerializeField]
    P_Stats _stats;
    [HideInInspector] 
    public P_Stats Stats;
    public Health Health { get; private set; } 
    public P_Movement Movement { get; private set; } 
    public P_SpellHandler Spells { get; private set; } 
    public P_LevelHandler Level { get; private set; } 
    public Canvas_Player_GUI_HUD UI { get; private set; }
    public P_CursorIcon CursorIcon { get; private set; }

    GameObject _child;  
    public void Initialize() => StartCoroutine(InitializeCoroutine());
    public IEnumerator InitializeCoroutine() {

        var color = GetComponentInChildren<SpriteRenderer>().color;
        color.a = 1;
        GetComponentInChildren<SpriteRenderer>().color = color;

        transform.position = Vector3.zero;
        _child = transform.GetChild(0).gameObject;
        _child.SetActive(false);

        canPlay = false;

        Rb = Rb == null ? GetComponent<Rigidbody2D>() : Rb;
        Animator = Animator == null ? GetComponent<Animator>() : Animator;

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
        if (this.CursorIcon == null)
            CursorIcon=GetComponent<P_CursorIcon>();



        yield return new WaitForSeconds(1.5f); 

        _child.SetActive(true);
        GetComponent<Animator>().SetTrigger("start");

        yield return new WaitForSeconds(.5f);

        canPlay = true;

        Level.Initialize(this);
        Movement.Initialize(this);
        Spells.Initialize(this);
        Health.Initialize();
        CursorIcon?.Initialize(this);

        yield return new WaitForSeconds(.25f);

        MainManager.Instance?.PlayerInitialized();
    }
    public void CantPlay() => canPlay = false;
 
}
