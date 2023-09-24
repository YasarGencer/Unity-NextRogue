using DG.Tweening;
 using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class B_Human_KnigthRush_Projectile: ANP_Projectile
{ 
    [SerializeField] List<Damager> _leftKnights;
    [SerializeField] List<Damager> _rightKnights;
    public override async void Initialize(Vector3 targetPos, float damage, float time, float speed, DOTInfo dotInfo) {
        base.Initialize(targetPos, 0, time, 0, dotInfo);


        foreach (var item in _leftKnights) {
            item.Initialize(damage, dotInfo);
            item.gameObject.transform.DOLocalMove(new Vector3(25, item.gameObject.transform.localPosition.y, item.gameObject.transform.localPosition.z), 6.5f).SetEase(Ease.InQuart);
        }
        foreach (var item in _rightKnights) {
            item.Initialize(damage, dotInfo);
            item.gameObject.transform.DOLocalMove(new Vector3(-25, item.gameObject.transform.localPosition.y, item.gameObject.transform.localPosition.z), 6.5f).SetEase(Ease.InQuart);
        }
        await Task.Delay(100);
        PlaySound();
    } 
}
