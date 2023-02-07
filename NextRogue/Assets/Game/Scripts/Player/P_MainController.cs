using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Windows;

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

    public void Start() {
        Initialize();
    }
    public void Initialize() {
        Rb = Rb == null ? GetComponent<Rigidbody2D>() : Rb;
        Animator = Animator == null ? GetComponent<Animator>() : Animator;


        this.Input = GetComponent<P_InputManager>();
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
}
