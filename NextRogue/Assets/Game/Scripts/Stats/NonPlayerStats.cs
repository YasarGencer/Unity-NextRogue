using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonPlayerStats : AStats {
    [Header("DAMAGE")]
    public float Damage;
    public float Range;
    public override void Initialize() {
        base.Initialize();
    }
    private void OnDrawGizmosSelected() {
        Debug.DrawLine(this.transform.position, this.transform.position + new Vector3(Range, 0, 0));
        Debug.DrawLine(this.transform.position, this.transform.position + new Vector3(-Range, 0, 0));
        Debug.DrawLine(this.transform.position, this.transform.position + new Vector3(0, Range, 0));
        Debug.DrawLine(this.transform.position, this.transform.position + new Vector3(0, -Range, 0));
    }
} 
