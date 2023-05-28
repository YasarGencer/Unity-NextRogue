using System.Threading.Tasks;
using UnityEngine;

public class __InitializeChildProjectile : AP_Projectile {  
    public override void Initialize(Vector3 targetPos, float damage, float time, float speed) {
        base.Initialize(targetPos, damage, time, speed); 
        InitChilds(Vector3.zero, damage, time, speed);
    }
    async void InitChilds(Vector3 targetPos, float damage, float time, float speed) {
        for (int i = 0; i < transform.childCount; i++) {
            await Task.Delay(100);
            if (transform.GetChild(0) != null) {
                var projectile = transform.GetChild(0).GetComponent<AProjectile>();
                projectile.Initialize(targetPos, damage, time, speed);
                projectile.transform.SetParent(null);
            }
        }
    }
}
