using System.Threading.Tasks;
using UnityEngine;

public class _IceBarrageProjectile : AP_Projectile {
    public override void Initialize(Vector3 mousePos, float damage, float time) {
        base.Initialize(mousePos, damage, time);
        InitChilds(mousePos, damage, time);
    }
    async void InitChilds(Vector3 mousePos, float damage, float time) {
        for (int i = 0; i < transform.childCount; i++) {
            transform.GetChild(i).GetComponent<_IceBarrageProjectile_shard>().Initialize(mousePos, damage, time);
            await Task.Delay(10);
        }
    }
}
