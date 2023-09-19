using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B_MainController : ANP_MainController {

    protected override void Initializator() {
        base.Initializator();

        UseSkill = GetComponent<B_Human_UseSkill>() as B_Human_UseSkill;
        Movement = GetComponent<B_MovementController>() as B_MovementController; 

        if (isTest == false) {
            Movement.Initialize(this);
            UseSkill.Initialize(this);

            if (this.CompareTag("Summoned"))
                Invoke("EndSummonLife", Stats.LifeSpan);
        }
    }
    public override void Die() { 
        base.Die();
    }
}
