using UnityEngine;

public class BloodshaperDrainBolt : AProjectile {
    public override void Initialize(Vector3 mousePos, float damage, float time, float speed) {
        var cellVial = MainManager.Instance.Player.GetChild(0).GetComponent<P_MainController>().Stats.SecondaryValue;
        MainManager.Instance.Player.GetChild(0).GetComponent<P_MainController>().Stats.SetSecondaryValue(0);
        base.Initialize(mousePos, damage * cellVial, time, speed);
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        Destroy(gameObject, .05F);
    } 
}
