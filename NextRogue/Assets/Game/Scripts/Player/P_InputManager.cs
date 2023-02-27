using UnityEngine.InputSystem;
using UnityEngine;

public class P_InputManager : MonoBehaviour {
    P_MainController _mainController;
    PlayerInput.OnMoveActions _inputOnMove;
    private void Awake() {
        _inputOnMove = new PlayerInput().OnMove;
    }
    public void Initialize(P_MainController mainController) {
        _mainController = mainController;
        SetEvents();
    }
    void SetEvents() {
        _inputOnMove.MOVE.performed += input => Direction(_inputOnMove.MOVE.ReadValue<Vector2>());
        _inputOnMove.MOVE.canceled += input => Direction(Vector2.zero);

        _inputOnMove.BASIC1.performed += input => Spell(0);
        _inputOnMove.BASIC2.performed += input => Spell(1);
        _inputOnMove.BASIC3.performed += input => Spell(2);
        _inputOnMove.BASIC4.performed += input => Spell(3);

        _inputOnMove.SPELL1.performed += input => Spell(4);
        _inputOnMove.SPELL2.performed += input => Spell(5);
        _inputOnMove.SPELL3.performed += input => Spell(6);
        _inputOnMove.SPELL4.performed += input => Spell(7);
        _inputOnMove.SPELL5.performed += input => Spell(8);
    }
    public Vector3 GetMouseWolrdPos() {
        Vector3 pos = Mouse.current.position.ReadValue();
        return new Vector3(Camera.main.ScreenToWorldPoint(pos).x, Camera.main.ScreenToWorldPoint(pos).y, 0);
    }
    public Vector3 GetMouseScreenPos() {
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
            _mainController.Movement.SetDirection(direction);
    }
    void Spell(int value) {
        if (_mainController.Spells)
            _mainController.Spells.Spell(value);
    }
}
