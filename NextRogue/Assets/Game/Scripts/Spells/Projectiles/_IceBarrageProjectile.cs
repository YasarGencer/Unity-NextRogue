using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.InputSystem;

public class _IceBarrageProjectile : APlayerProjectile {
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
