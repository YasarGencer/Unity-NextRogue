using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using static UnityEngine.ParticleSystem;
using static UnityEngine.RuleTile.TilingRuleOutput;

[CreateAssetMenu(fileName = "GolemAreaAttack", menuName = "ScriptableObjects/EnemySpells/GolemAreaAttack")]

public class GolemAreaAttack : ANP_Spell
{

   
    IDisposable _moveRX;
    public override void Initialize(ANP_MainController mainController)
    {
        base.Initialize(mainController);
    }
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
        Instantiate(Spell, _mainController.transform).GetComponent<AProjectile>().Initialize(_mainController.transform.position,Damage,CooldownTime,Speed,DOTInfo);
    }
   
}
