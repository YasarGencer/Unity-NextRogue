using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

[CreateAssetMenu(fileName = "NecromancerConsume", menuName = "ScriptableObjects/Spells/NecromancerConsume")]
public class NecromencerConsume : ASpell {
    [Header("Consume")]
    public float DefaultHeal;
    public float HealPerCorpse;
    public float CorpseRange;
    public GameObject Particle;
    public override void Initialize(P_MainController mainController, int value) {
        base.Initialize(mainController, value);
    }
    public override void ActivateSpell() {
        base.ActivateSpell();
        var count = CheckCorpses();
        var heal = DefaultHeal + (count * HealPerCorpse);
        _mainController.Health.GainHealth(heal);
    }
    int CheckCorpses() {
        var corpses = GameObject.FindGameObjectsWithTag("Corpse");
        var count = 0;
        foreach (var corpse in corpses) {
            var dist = Vector2.Distance(corpse.transform.position, _mainController.transform.position);
            if (dist <= CorpseRange) {
                count++;
                Destroy(Instantiate(Particle, corpse.transform.position, Quaternion.identity), .5f);
                GameObject.Destroy(corpse);
            }
        }
        return count;
    }
}
