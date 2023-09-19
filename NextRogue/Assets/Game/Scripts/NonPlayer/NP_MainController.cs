using System.Collections;
using UnityEngine;

public class NP_MainController : ANP_MainController
{
    public Vector2 PatrolTarget { get; protected set; }

    protected override void Initializator() {
        base.Initializator();

        UseSkill = GetComponent<ANP_Use_Skill>() as ANP_Use_Skill;
        Movement = GetComponent<ANP_Movement>() as ANP_Movement;
        Attack = GetComponent<ANP_Attack>() as ANP_Attack;

        if (isTest == false) { 
            if (Movement != null)
                Movement.Initialize(this);
            if (Attack != null)
                Attack.Initialize(this);
            if (UseSkill != null)
                UseSkill.Initialize(this);

            if (this.CompareTag("Summoned"))
                Invoke("EndSummonLife", Stats.LifeSpan);
        }
    }
    public override void Die() {
        Movement.Die();
        Attack.Die();
        base.Die();
    }
}
