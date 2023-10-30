using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B_MainController : ANP_MainController {
    B_PhaseController phaseController; 
    public B_PhaseController PhaseController { get { return phaseController; } private set { phaseController = value; } } 
    protected override void Initializator() {
        base.Initializator();

        UseSkill = GetComponent<B_Human_UseSkill>() as B_Human_UseSkill;
        Movement = GetComponent<B_MovementController>() as B_MovementController;
        PhaseController = GetComponent<B_PhaseController>() as B_PhaseController;
        Attack = GetComponent<ANP_Attack>() as ANP_Attack; 

        if (isTest == false) {
            Movement.Initialize(this);
            UseSkill.Initialize(this);
            PhaseController.Initialize(this);
            Attack.Initialize(this);

            if (this.CompareTag("Summoned"))
                Invoke("EndSummonLife", Stats.LifeSpan);
        }
    }
    public override void Die() { 
        base.Die();
    }
}
