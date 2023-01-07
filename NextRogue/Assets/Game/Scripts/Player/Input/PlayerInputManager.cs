using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour {
    PlayerMainController _mainController;
    PlayerInput.OnMoveActions _inputOnMove;
    public void Initialize(PlayerMainController mainController) {
        _mainController = mainController;
        _inputOnMove = new PlayerInput().OnMove;
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
        if (_mainController.Movement)
            _mainController.Movement.Direction(direction);
    }
    void Dash() {
        if (_mainController.Movement)
            _mainController.Movement.Dash();
    }
    void Basic(int value) {
        if (_mainController.BasicAttacks)
            _mainController.BasicAttacks.Basic(value);
    }
}
