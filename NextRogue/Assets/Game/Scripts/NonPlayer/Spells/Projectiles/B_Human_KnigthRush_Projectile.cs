using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B_Human_KnigthRush_Projectile: ANP_Projectile
{ 
    [SerializeField] List<Damager> _leftKnights;
    [SerializeField] List<Damager> _rightKnights;
    public override void Initialize(Vector3 targetPos, float damage, float time, float speed, DOTInfo dotInfo) {
        base.Initialize(targetPos, 0, time, 0, dotInfo);

        foreach (var item in _leftKnights) {
            item.Initialize(_damage, dotInfo);
            item.gameObject.transform.DOLocalMove(new Vector3(10, item.gameObject.transform.localPosition.y, item.gameObject.transform.localPosition.z), 3f).SetEase(Ease.InExpo);
        }
        foreach (var item in _rightKnights) {
            item.Initialize(_damage, dotInfo);
            item.gameObject.transform.DOLocalMove(new Vector3(-10, item.gameObject.transform.localPosition.y, item.gameObject.transform.localPosition.z), 3f).SetEase(Ease.InExpo);
        }
    } 
}
