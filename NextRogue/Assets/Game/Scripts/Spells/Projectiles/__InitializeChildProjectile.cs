using System.Threading.Tasks;
using UnityEngine;

public class __InitializeChildProjectile : AP_Projectile {  
    public override void Initialize(Vector3 targetPos, float damage, float time, float speed, DOTInfo dotInfo) {
        base.Initialize(targetPos, damage, time, speed, dotInfo);
        if (CompareTag("EnemyProjectile"))
            gameObject.layer = 13;
        else
            gameObject.layer = 6;
        InitChilds(Vector3.zero, damage, time, speed, dotInfo);
    }
    async void InitChilds(Vector3 targetPos, float damage, float time, float speed, DOTInfo dotInfo) {

        var count = transform.childCount;
        for (int i = 0; i < count; i++) {
            await Task.Delay(50);
            if (transform.childCount > 0) {
                var projectile = transform.GetChild(0).GetComponent<AProjectile>();
                projectile.gameObject.SetActive(true);

                if (CompareTag("EnemyProjectile")) {
                    projectile.gameObject.layer = 13;
                    projectile.tag = "EnemyProjectile";
                } else {
                    projectile.gameObject.layer = 6;
                    projectile.tag = "PlayerProjectile";
                }

                projectile.Initialize(targetPos, damage, time, speed, dotInfo);
                projectile.transform.SetParent(null);
            }
        }
    }
}
