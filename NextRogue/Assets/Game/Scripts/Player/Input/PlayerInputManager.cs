using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerInputManager : MonoBehaviour {
    PlayerMainController _mainController;
    PlayerInput.OnMoveActions _inputOnMove;
    private void Awake() {
        _inputOnMove = new PlayerInput().OnMove;
    }
    public void Initialize(PlayerMainController mainController) {
        _mainController = mainController;
        SetEvents();
    }
    void SetEvents() {
        _inputOnMove.MOVE.performed += input => Direction(_inputOnMove.MOVE.ReadValue<Vector2>());
        _inputOnMove.MOVE.canceled += input => Direction(Vector2.zero);

        _inputOnMove.BASIC1.performed += input => Basic(0);
        _inputOnMove.BASIC2.performed += input => Basic(1);
        _inputOnMove.BASIC3.performed += input => Basic(2);
        _inputOnMove.BASIC4.performed += input => Basic(3);

        _inputOnMove.SPELL1.performed += input => Spell(0);
        _inputOnMove.SPELL2.performed += input => Spell(1);
        _inputOnMove.SPELL3.performed += input => Spell(2);
        _inputOnMove.SPELL4.performed += input => Spell(3);
        _inputOnMove.SPELL5.performed += input => Spell(4);
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
            _mainController.Movement.GetDirection(direction);
    }
    void Basic(int value) {
        if (_mainController.Spells)
            _mainController.Spells.Basic(value);
    }
    void Spell(int value) {
        if (_mainController.Spells)
            _mainController.Spells.Spell(value);
    }
}
