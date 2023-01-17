using System;
using System.Collections;
using System.Threading;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UIElements;

public class NecromancerGraveKeeperProjectile : APlayerProjectile {
    [SerializeField] float _range;
    public override void Initialize(Vector3 mousePos, float damage, float time) {
        base.Initialize(mousePos, damage, time);
        transform.localScale = new Vector3(_range, _range, _range);
        Summon();
    }
    void Summon() {
        foreach (var item in GameObject.FindGameObjectsWithTag("Corpse")) {
            if (Vector2.Distance(item.transform.position, transform.position) < _range) {
                Instantiate(item.GetComponent<Corpse>().GetCorpse(), item.transform.position, Quaternion.identity);
                Destroy(item.gameObject);
            }
        }
    }
}