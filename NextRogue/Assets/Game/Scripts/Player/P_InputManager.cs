using UnityEngine.InputSystem;
using UnityEngine;

public class P_InputManager : MonoBehaviour {
    P_MainController _mainController;
    PlayerInput.OnMoveActions _inputOnMove;
    bool _isPaused = false;
    private void Awake() {
        var playerInput = new PlayerInput();
        _inputOnMove = playerInput.OnMove;
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

        _inputOnMove.PAUSE.performed += input => RunPause();
    }
    public string GetKeyInfo() {
        var b1 = _inputOnMove.BASIC1.GetBindingDisplayString();
        var b2 = _inputOnMove.BASIC2.GetBindingDisplayString();
        var b3 = _inputOnMove.BASIC3.GetBindingDisplayString();
        var b4 = _inputOnMove.BASIC4.GetBindingDisplayString();

        var c1 = _inputOnMove.SPELL1.GetBindingDisplayString();
        var c2 = _inputOnMove.SPELL2.GetBindingDisplayString();
        var c3 = _inputOnMove.SPELL3.GetBindingDisplayString();
        var c4 = _inputOnMove.SPELL4.GetBindingDisplayString();
        var c5 = _inputOnMove.SPELL5.GetBindingDisplayString();

        var s = "/";
        return b1 + s + b2 + s + b3 + s + b4 + s + c1 + s + c2 + s + c3 + s + c4 + s + c5;
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
        if (MainManager.Instance.GameManager.GamePaused)
            return;
        if (_mainController.Movement)
            _mainController.Movement.SetDirection(direction);
    }
    void Spell(int value) {
        if (MainManager.Instance.GameManager.GamePaused)
            return;
        if (_mainController.Spells)
            _mainController.Spells.Spell(value);
    }
    void RunPause() {
        if(MainManager.Instance.GameManager.GamePaused)
            MainManager.Instance.EventManager.RunOnGameUnPuase();
        else
            MainManager.Instance.EventManager.RunOnGamePause();
    }  
}
