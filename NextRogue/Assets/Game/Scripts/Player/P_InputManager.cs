using UnityEngine.InputSystem;
using UnityEngine;
using System;

public class P_InputManager : MonoBehaviour {
    bool _isInit;
    P_MainController _mainController;
    PlayerInput.OnMoveActions _inputOnMove;
    bool _isPaused = false;
    private void Awake() {
        var playerInput = new PlayerInput();
        _inputOnMove = playerInput.OnMove;
    }
    public void Initialize(P_MainController mainController) {
        if (_isInit)
            return;
        _isInit = true;
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
        _inputOnMove.INTERACT.performed += input => RunInteract();
    }


    public string GetKeyInfo() {
        var s = "/";
        string skillText = "";
        for (int i = 0; i < 9; i++)
            skillText += GetSkillKey(i) + s; 
        return skillText;
    }
    public string GetSkillKey(int value) {
        switch (value) {
            case 0:
                return _inputOnMove.BASIC1.GetBindingDisplayString();
            case 1:
                return _inputOnMove.BASIC2.GetBindingDisplayString();
            case 2:
                return _inputOnMove.BASIC3.GetBindingDisplayString();
            case 3:
                return _inputOnMove.BASIC4.GetBindingDisplayString();
            case 4:
                return _inputOnMove.SPELL1.GetBindingDisplayString();
            case 5:
                return _inputOnMove.SPELL2.GetBindingDisplayString();
            case 6:
                return _inputOnMove.SPELL3.GetBindingDisplayString();
            case 7:
                return _inputOnMove.SPELL4.GetBindingDisplayString();
            case 8:
                return _inputOnMove.SPELL5.GetBindingDisplayString();
            default:
                return null;
        }
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
        if (_mainController.canPlay)
            if (_mainController.Movement)
            _mainController.Movement.SetDirection(direction);
    }
    void Spell(int value) { 
        if (MainManager.Instance.GameManager.GamePaused)
            return;
        if (_mainController.canPlay)
            if (_mainController.Spells)
                _mainController.Spells.Spell(value);  
    }
    void RunPause() {
        if (_mainController.canPlay) {
            if (MainManager.Instance.GameManager.GamePaused)
                MainManager.Instance.EventManager.RunOnGameUnPuase();
            else
                MainManager.Instance.EventManager.RunOnGamePause();
        }
    }
    private void RunInteract() {
        MainManager.Instance.EventManager.RunOnInteract();
    }

}
