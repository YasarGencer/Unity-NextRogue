using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

[CreateAssetMenu(fileName = "SlimeBloodConsumption", menuName = "ScriptableObjects/EnemySpells/SlimeBloodConsumption")]

public class SlimeBloodConsumption : ANP_Spell
{
    public int MaxCell = 5;
    public float BloodRange;
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
        
        int particleCount = CheckBloods();
        _mainController.transform.localScale = Vector3.one * (_mainController.Stats.Health / 20);

        _mainController.Stats.MaxHealth+=3/2*particleCount;
        _mainController.Health.GainHealth(3/2*particleCount);
        Debug.Log("particleCount: "+particleCount+"  maxhealt-Health "+ _mainController.Stats.MaxHealth+" - "+ _mainController.Stats.Health);

        //_mainController.Health.GainHealth(_mainController.Stats.MaxHealth / 4);
        //

    }
    int CheckBloods()
    {
        var bloods = GameObject.FindGameObjectsWithTag("Blood");
        var count = 0;
        foreach (var item in bloods)
        {
            var dist = Vector2.Distance(item.transform.position, _mainController.transform.position);
            if (dist <= BloodRange)
            {
                count++;
                Destroy(Instantiate(Particle, item.transform.position, Quaternion.identity), .5f);
                GameObject.Destroy(item);
            }
        }
        return count;
    }

}
