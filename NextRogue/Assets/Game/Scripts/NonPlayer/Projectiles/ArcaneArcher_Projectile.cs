using System.Threading.Tasks;
using UnityEngine;

public class ArcaneArcher_Projectile : ANP_Projectile {
    public override void Initialize(Vector3 targetPos, float damage, float time) {
        base.Initialize(targetPos, damage, time);
        InitChilds(damage);
    }
    async void InitChilds(float damage) {
        for (int i = 0; i < transform.childCount; i++) {
            transform.GetChild(i).GetComponent<ArcaneArcher_Projectile_shard>().Initialize(damage);
            await Task.Delay(25);
        }
    }
}