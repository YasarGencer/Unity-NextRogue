using System;
using System.Collections;
using System.Collections.Generic;
using UniRx.Triggers;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Spells", menuName = "ScriptableObjects/Basics/BasicDash", order = 0)]
public class BasicDash : ASpell
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
    public void CallStopDash(BasicDash basicDash, float time) {
        StartCoroutine(StopDash(basicDash, time));
    }
    public void CallDashEffect(PlayerMainController mainController, float dashTime) {
        StartCoroutine(DashEffect(mainController, dashTime));
    }
    IEnumerator StopDash(BasicDash basicDash, float time) {
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
        dashParticle.transform.localScale = mainController.transform.localScale;
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
