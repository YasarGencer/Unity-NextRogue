using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    PlayerInput.OnMoveActions _inputOnMove;

    PlayerMovement _playerMovement;
    private void Awake() => Initialize();
    public void Initialize() {
        _inputOnMove = new PlayerInput().OnMove;

        _playerMovement = GetComponent<PlayerMovement>();
        _playerMovement.Initialize();

        SetEvents();
    }
    void SetEvents() {
        _inputOnMove.DASH.performed += input => Dash();
        _inputOnMove.MOVE.performed += input => Direction(_inputOnMove.MOVE.ReadValue<Vector2>());
        _inputOnMove.MOVE.canceled += input => Direction(Vector2.zero);
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
        if (_playerMovement)
            _playerMovement.Direction(direction);
    }
    void Dash() {
        if (_playerMovement)
            _playerMovement.Dash();
    }
}
