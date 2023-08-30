using UnityEngine;

public class __ShotProjectile : AProjectile {
    [SerializeField] bool _setActiveOnCollision = false;
    public override void Initialize(Vector3 mousePos, float damage, float time, float speed, DOTInfo dotInfo) {
        base.Initialize(mousePos, damage, time, speed, dotInfo);
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        PlaySound();
        gameObject.SetActive(_setActiveOnCollision); 
    } 
}
