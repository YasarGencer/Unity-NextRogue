using System;
using System.Collections;
using System.Collections.Generic;
using UniRx.Triggers;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "NecromancerNecroticDash", menuName = "ScriptableObjects/Spells/NecromancerNecroticDash")]
public class NecromancerNecroticDash : ASpell
{
    [Header("DASH")]
    public float DashForce;
    public float DashTime;
    public float CorpseRange;
    public float ForceMultiplier;
    public GameObject Particle;
    public override void Initialize(P_MainController mainController, int value) {
        base.Initialize(mainController, value);
    }
    public override void ActivateSpell() {
        base.ActivateSpell();
        Vector2 direction = _mainController.Movement.Direction == Vector2.zero ? new Vector2(1, 0) : _mainController.Movement.Direction;

        var count = CheckCorpses();
        var rb = _mainController.Rb;
        var dashforce = DashForce * (math.pow(ForceMultiplier, count));
        dashforce = UnityEngine.Mathf.Clamp(dashforce, 0, DashForce * 3);

        var force = direction * dashforce * rb.mass;
        rb.AddForce(force,ForceMode2D.Impulse); 

        DashEffect();

        BasicDashMono.Initialize(DashTime + 1);
        BasicDashMono.Instance.GetComponent<BasicDashMono>().CallStopDash(_mainController, DashTime);
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
    void DashEffect() {
        //variables
        GameObject dashParticle = new GameObject();
        SpriteRenderer sprite;
        Color color;

        //effects
        dashParticle.name = "dashParticle";
        dashParticle.transform.position = _mainController.transform.GetChild(0).position;
        dashParticle.transform.localScale = _mainController.transform.GetChild(0).localScale;
        dashParticle.AddComponent<SpriteRenderer>();

        sprite = dashParticle.GetComponent<SpriteRenderer>();
        sprite.sprite = _mainController.GetComponentInChildren<SpriteRenderer>().sprite;

        color = sprite.color;
        color.a = .25f;
        sprite.color = color;

        Destroy(dashParticle, .25f);
    }
}
class BasicDashMono : MonoBehaviour {
    public static GameObject Instance;
    public static void Initialize(float time) {
        Instance = new GameObject();
        Instance.AddComponent<BasicDashMono>();
        Destroy(Instance, time);
    }
    public void CallStopDash(P_MainController mainController, float time) {
        StartCoroutine(StopDash(mainController,time));
    }
    IEnumerator StopDash(P_MainController mainController, float time) {
        yield return new WaitForSeconds(time); 
        mainController.Rb.velocity = Vector2.zero;
    }
    
}
