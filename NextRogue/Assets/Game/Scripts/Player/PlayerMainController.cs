using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Windows;

public class PlayerMainController : MonoBehaviour {
    
    [HideInInspector]
    public Animator Animator;
    [HideInInspector]
    public Rigidbody2D Rb;


    [HideInInspector]
    public PlayerInputManager Input;
    [HideInInspector]
    public PlayerStats Stats;
    [HideInInspector]
    public Health Health;
    [HideInInspector]
    public PlayerMovement Movement;
    [HideInInspector]
    public PlayerSpells Spells;
    [HideInInspector]
    public PlayerUI UI;

    public void Awake() {
        Initialize();
    }
    public void Initialize() {
        Rb = Rb == null ? GetComponent<Rigidbody2D>() : Rb;
        Animator = Animator == null ? GetComponent<Animator>() : Animator;


        this.Input = GetComponent<PlayerInputManager>();
        Stats = GetComponent<PlayerStats>();
        Health = GetComponent<Health>();
        Movement = GetComponent<PlayerMovement>();
        Spells = GetComponent<PlayerSpells>();
        UI = GetComponent<PlayerUI>();

        this.Input.Initialize(this);
        Stats.Initialize();
        Movement.Initialize(this);
        Spells.Initialize(this);
        UI.Initialize(this);
        Stats.Initialize(); 
        Health.Initialize();
    }
}
