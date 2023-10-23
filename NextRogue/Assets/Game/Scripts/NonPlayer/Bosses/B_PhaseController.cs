using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B_PhaseController : MonoBehaviour
{
    B_MainController mainController;
    [Header("Leave as default, only change the one you want to effect phase")]
    [SerializeField]
    [Range(0, 1)] float healthPercent = 1;
    public void Initialize(B_MainController b_MainController) {
        mainController = b_MainController;
    } 
    public void HealthPhaser() {
        if (healthPercent == 1)
            return;
        if (mainController == null)
            return;
        if (mainController.Stats.Health <= healthPercent * mainController.Stats.MaxHealth)
            BossManager.StaticNextPhase();
    }
}
