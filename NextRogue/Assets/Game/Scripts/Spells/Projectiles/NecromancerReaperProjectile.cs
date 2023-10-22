using UnityEngine;

public class NecromancerReaperProjectile : AP_Projectile {
    Transform player;
    public override void Initialize(Vector3 mousePos, float damage, float time, float speed, DOTInfo dotInfo) {
        base.Initialize(mousePos, damage, time * 2, speed, dotInfo);
        player = MainManager.Instance.Player.GetChild(0);
    }
    private void Update() {
        transform.position = player.position;
    }
}
