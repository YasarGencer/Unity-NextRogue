using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Stats : AStats {

    [Header("Summons")]
    public float MaxSummonControlRange;
    public float MinSummonControlRange;
    public override void Initialize() {
        base.Initialize();
    }
    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, MaxSummonControlRange);
        Gizmos.DrawWireSphere(transform.position, MinSummonControlRange);
    }
}
