using System;
using System.Collections;
using System.Collections.Generic;
using UniRx.Triggers;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "NecromancerNecroticDash", menuName = "ScriptableObjects/Spells/NecromancerNecroticDash")]
public class NecromancerNecroticDash : ASpell
{
    [Header("DASH")]
    public float DashForce;
    public float DashTime;
    public override void Initialize(PlayerMainController mainController, int value) {
        base.Initialize(mainController, value);
    }
    public override void ActivateSpell() {
        base.ActivateSpell();

        _mainController.Rb.velocity = _mainController.Movement.Direction * DashForce;

        //visual effects and cooldowns
        BasicDashMono.Initialize(CooldownTime);
        BasicDashMono.Instance.GetComponent<BasicDashMono>().CallStopDash(this, DashTime);
        BasicDashMono.Instance.GetComponent<BasicDashMono>().CallDashEffect(_mainController, DashTime);
    }
}
class BasicDashMono : MonoBehaviour {
    public static GameObject Instance;
    public static void Initialize(float time) {
        Instance = new GameObject();
        Instance.AddComponent<BasicDashMono>();
        Destroy(Instance, time);
    }
    public void CallStopDash(NecromancerNecroticDash basicDash, float time) {
        StartCoroutine(StopDash(basicDash, time));
    }
    public void CallDashEffect(PlayerMainController mainController, float dashTime) {
        StartCoroutine(DashEffect(mainController, dashTime));
    }
    IEnumerator StopDash(NecromancerNecroticDash basicDash, float time) {
        yield return new WaitForSeconds(time);
        basicDash.MainController.Rb.velocity = Vector2.zero;
        StopCoroutine(StopDash(basicDash, time));
    }
    IEnumerator DashEffect(PlayerMainController mainController, float dashTime) {
        //variables
        GameObject dashParticle = new GameObject();
        SpriteRenderer sprite;
        Color color;

        //effects
        dashParticle.name = "dashParticle";
        dashParticle.transform.position = mainController.transform.GetChild(0).position;
        dashParticle.transform.localScale = mainController.transform.GetChild(0).localScale;
        dashParticle.AddComponent<SpriteRenderer>();

        sprite = dashParticle.GetComponent<SpriteRenderer>();
        sprite.sprite = mainController.GetComponentInChildren<SpriteRenderer>().sprite;

        color = sprite.color;
        color.a = .25f;
        sprite.color = color;

        Destroy(dashParticle, .25f);

        yield return new WaitForSeconds(dashTime / 7);
        //check if dash should go on
        if (mainController.Rb.velocity != Vector2.zero)
            StartCoroutine(DashEffect(mainController, dashTime));
    }
    
}
