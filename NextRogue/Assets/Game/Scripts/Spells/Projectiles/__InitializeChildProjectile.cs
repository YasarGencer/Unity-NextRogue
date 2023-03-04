using System.Threading.Tasks;
using UnityEngine;

public class __InitializeChildProjectile : AP_Projectile {  
    public override void Initialize(Vector3 targetPos, float damage, float time, float speed) {
        base.Initialize(targetPos, damage, time, speed); 
        InitChilds(Vector3.zero, damage, time, speed);
    }
    async void InitChilds(Vector3 targetPos, float damage, float time, float speed) {
        for (int i = 0; i < transform.childCount; i++) {
            var projectile = transform.GetChild(i).GetComponent<AProjectile>();
            projectile.Initialize(targetPos, damage, time, speed); 
            await Task.Delay(50);
        }
    }
}
