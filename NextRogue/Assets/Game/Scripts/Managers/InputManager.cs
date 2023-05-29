﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    bool isInit;
    public InputActions.GameActions _input; 

    P_MainController _mainController;

    private void Awake() {
        var input = new InputActions(); 
        _input = input.Game; 
    }
    #region ENABLES
    private void OnEnable() {
        _input.Enable();
    }
    private void OnDisable() {
        _input.Disable();
    }
    #endregion
    public void Initialize() {
        if (isInit)
            return;
        isInit = true; 
        var player = MainManager.Instance.Player.GetChild(0);
        if(player!= null )
            _mainController = player.GetComponent<P_MainController>();

        SetEvents();
    }
    void SetEvents() {
        InGame();
        Mainmenu();
        Paused();
        General();
    }

    private void General() { 
        _input.PAUSE.performed += general => RunPause(); 
        _input.INTERACT.performed += general => RunInteract(); 
    }

    private void Paused() { 
    }

    private void Mainmenu() { 
    }

    private void InGame() {
        _input.MOVE.performed += inGame => Direction(_input.MOVE.ReadValue<Vector2>());
        _input.MOVE.canceled += inGame => Direction(Vector2.zero);

        _input.BASIC1.performed += inGame => Spell(0);
        _input.BASIC2.performed += inGame => Spell(1);
        _input.BASIC3.performed += inGame => Spell(2);
        _input.BASIC4.performed += inGame => Spell(3);

        _input.SPELL1.performed += inGame => Spell(4);
        _input.SPELL2.performed += inGame => Spell(5);
        _input.SPELL3.performed += inGame => Spell(6);
        _input.SPELL4.performed += inGame => Spell(7);
        _input.SPELL5.performed += inGame => Spell(8);

    }
    #region KEY INFO
    public string GetKeyInfo() {
        var s = "/";
        string skillText = "";
        for (int i = 0; i < 9; i++)
            skillText += GetSkillKeys(i) + s;
        return skillText;
    }
    public string GetSkillKeys(int value) {
        switch (value) {
            case 0:
                return GetAnyKey(_input.BASIC1);
            case 1:
                return GetAnyKey(_input.BASIC2);
            case 2:
                return GetAnyKey(_input.BASIC3);
            case 3:
                return GetAnyKey(_input.BASIC4);
            case 4:
                return GetAnyKey(_input.SPELL1);
            case 5:
                return GetAnyKey(_input.SPELL2);
            case 6:
                return GetAnyKey(_input.SPELL3);
            case 7:
                return GetAnyKey(_input.SPELL4);
            case 8:
                return GetAnyKey(_input.SPELL5);
            default:
                return null;
        }
    }
    public string GetAnyKey(InputAction action) {
        return action.GetBindingDisplayString();
    }
    public void SetAnyKey(InputAction action, Key key) {
        action.ChangeBinding(key.ToString()); 
    }

    #endregion
    public Vector3 GetMouseWolrdPos() {
        Vector3 pos = Mouse.current.position.ReadValue();
        return new Vector3(Camera.main.ScreenToWorldPoint(pos).x, Camera.main.ScreenToWorldPoint(pos).y, 0);
    }
    public Vector3 GetMouseScreenPos() {
        return Mouse.current.position.ReadValue();
    }
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
        if(_mainController.canPlay)
            MainManager.Instance.EventManager.RunOnInteract();
    }
}
