using System.Threading.Tasks;
using UnityEngine;

public class _IceBarrageProjectile : AP_Projectile {
    public override void Initialize(Vector3 targetPos, float damage, float time) {
        base.Initialize(targetPos, damage, time);
        InitChilds(targetPos, damage, time);
    }
    async void InitChilds(Vector3 targetPos, float damage, float time) {
        for (int i = 0; i < transform.childCount; i++) {
            transform.GetChild(i).GetComponent<AP_Projectile>().Initialize(targetPos, damage, time);
            await Task.Delay(10);
        }
    }
}
