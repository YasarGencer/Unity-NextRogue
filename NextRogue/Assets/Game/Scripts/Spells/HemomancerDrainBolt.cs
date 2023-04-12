using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class HemomancerDrainBolt : AProjectile {
    public override void Initialize(Vector3 mousePos, float damage, float time, float speed) {
        var cellVial = GameObject.FindObjectOfType<P_MainController>().Stats.SecondaryValue;
        GameObject.FindObjectOfType<P_MainController>().Stats.SetSecondaryValue(0);
        base.Initialize(mousePos, damage * cellVial, time, speed);
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        Destroy(gameObject, .05F);
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        Destroy(gameObject, .05f);
    }
}
