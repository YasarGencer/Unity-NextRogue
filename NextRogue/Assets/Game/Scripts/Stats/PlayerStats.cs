using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : AStats {

    [Header("DASH")]
    public float DashForce;
    public float DashTime;
    public float DashCooldown;
    public int MaxDashes;
    [Header("DAMAGE")]
    public float BasicCooldown;
    public float Basic1Damage;
    public float Basic2Damage;

    public override void Initialize() {
        base.Initialize();
    }
}
