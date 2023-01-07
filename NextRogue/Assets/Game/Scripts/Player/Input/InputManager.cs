using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    PlayerInput.OnMoveActions _inputOnMove;

    [HideInInspector]
    public PlayerStats PlayerStats;
    [HideInInspector]
    public PlayerMovement PlayerMovement;
    [HideInInspector]
    public PlayerBasicAttacks PlayerBasicAttacks;
    [HideInInspector]
    public Health PlayerHealth;

    [HideInInspector]
    public Animator Animator;
    [HideInInspector]
    public Rigidbody2D Rb;


    private void Awake() => Initialize();
    public void Initialize() {
        _inputOnMove = new PlayerInput().OnMove;


        Rb = Rb == null ? GetComponent<Rigidbody2D>() : Rb;
        Animator = Animator == null ? GetComponent<Animator>() : Animator;

        PlayerStats = GetComponent<PlayerStats>();
        PlayerBasicAttacks = GetComponent<PlayerBasicAttacks>();
        PlayerMovement = GetComponent<PlayerMovement>();
        PlayerHealth = GetComponent<Health>();


        PlayerStats.Initialize(this);
        PlayerMovement.Initialize(this);
        PlayerBasicAttacks.Initialize(this);
        PlayerHealth.Initialize();

        SetEvents();
    }
    void SetEvents() {
        _inputOnMove.DASH.performed += input => Dash();
        _inputOnMove.MOVE.performed += input => Direction(_inputOnMove.MOVE.ReadValue<Vector2>());
        _inputOnMove.MOVE.canceled += input => Direction(Vector2.zero);
        _inputOnMove.BASICATTACK1.performed += input => Basic(1);
        _inputOnMove.BASICATTACK2.performed += input => Basic(2);
    }
    public Vector2 GetMousePos() {
        return Mouse.current.position.ReadValue();
    }

    #region ENABLES
    private void OnEnable() {
        _inputOnMove.Enable();
    }
    private void OnDisable() {
        _inputOnMove.Disable();
    }
    #endregion
    //EVENTS
    void Direction(Vector2 direction) {
        if (PlayerMovement)
            PlayerMovement.Direction(direction);
    }
    void Dash() {
        if (PlayerMovement)
            PlayerMovement.Dash();
    }
    void Basic(int value) {
        if (PlayerBasicAttacks)
            PlayerBasicAttacks.Basic(value);
    }
}
