using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

[CreateAssetMenu(fileName = "GolemAreaAttack", menuName = "ScriptableObjects/EnemySpells/GolemAreaAttack")]

public class GolemAreaAttack : ANP_Spell
{

    public GameObject Particle;

    public override bool CheckConditions(ANP_MainController mainController)
    {
        if (base.CheckConditions(mainController) == false)
            return false;
        if (_mainController.Distance(_mainController.Target.Target.transform) > UseRange)
            return false;
        return true;

    }
    public override void ActivateSpell()
    {
        base.ActivateSpell();

    }
    void StoneSmash()
    {
       // Collider[] colliders = Physics.OverlapSphere();
    }
    

}
