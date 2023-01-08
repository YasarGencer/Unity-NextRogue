using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows;

public class PlayerMainController : MonoBehaviour {
    [HideInInspector]
    public PlayerInputManager Input;
    [HideInInspector]
    public PlayerStats Stats;
    [HideInInspector]
    public PlayerMovement Movement;
    [HideInInspector]
    public PlayerBasicAttacks BasicAttacks;
    [HideInInspector]
    public PlayerSpells Spells;
    [HideInInspector]
    public Health Health;

    [HideInInspector]
    public Animator Animator;
    [HideInInspector]
    public Rigidbody2D Rb;
    private void Awake() => Initialize();
    public void Initialize() {

        Rb = Rb == null ? GetComponent<Rigidbody2D>() : Rb;
        Animator = Animator == null ? GetComponent<Animator>() : Animator;

        this.Input = GetComponent<PlayerInputManager>();
        Stats = GetComponent<PlayerStats>();
        Movement = GetComponent<PlayerMovement>();
        BasicAttacks = GetComponent<PlayerBasicAttacks>();
        Spells = GetComponent<PlayerSpells>();
        Health = GetComponent<Health>();

        this.Input.Initialize(this);
        Stats.Initialize();
        Movement.Initialize(this);
        BasicAttacks.Initialize(this);
        Spells.Initialize(this);
        Health.Initialize();
    }
}
