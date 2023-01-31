using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class APlayerProjectile : MonoBehaviour
{
    protected Vector3 _mousePos;
    protected float _damage;
    public virtual void Initialize(Vector3 mousePos, float damage, float time) {
        Destroy(gameObject, time);

        _damage = damage;
        if (_damage > 0) {
            Damager damager = GetComponent<Damager>() as Damager;
            if (damager)
                GetComponent<Damager>().Initialize(_damage);
        }
        if(mousePos != null) {
            _mousePos = mousePos;
            SetRotation();
        }
    }
    void SetRotation() {
        _mousePos.z = 0f;

        _mousePos.x -= transform.position.x;
        _mousePos.y -= transform.position.y;

        float angle = Mathf.Atan2(_mousePos.y, _mousePos.x) * Mathf.Rad2Deg;
        transform.localRotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}
