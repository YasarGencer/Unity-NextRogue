using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class B_Human_ArrowRain_Projectile : ANP_Projectile { 
    public override void Initialize(Vector3 targetPos, float damage, float time, float speed, DOTInfo dotInfo) {
        base.Initialize(targetPos, damage, time, speed, dotInfo);


        //GetComponent<Damager>().Initialize(damage, dotInfo);
        //PlaySound();
    } 
}
