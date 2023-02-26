using UnityEngine;

public abstract class ANP_Projectile : MonoBehaviour
{
    protected float _damage;
    public virtual void Initialize(Vector3 targetPos, float damage, float time) {
        if (gameObject)
            Destroy(gameObject, time);

        _damage = damage;
        if (_damage > 0) {
            Damager damager = GetComponent<Damager>() as Damager;
            if (damager)
                GetComponent<Damager>().Initialize(_damage);
        }
        SetRotation(targetPos);
    }
    void SetRotation(Vector3 targetPos) {
        targetPos.z = 0f;

        targetPos.x -= transform.position.x;
        targetPos.y -= transform.position.y;

        float angle = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg;
        transform.localRotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}
