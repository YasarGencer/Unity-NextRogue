using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class APlayerBasicProjectile : MonoBehaviour
{
    protected Vector3 _mousePos;
    protected float _damage;
    public virtual void Initialize(Vector3 mousePos, float damage) {
        Destroy(gameObject, 4f);

        _damage = damage;     
        GetComponent<Damager>().Initialize(_damage);

        _mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        SetRotation();
    }
    void SetRotation() {
        _mousePos.z = 0f;

        _mousePos.x -= transform.position.x;
        _mousePos.y -= transform.position.y;

        float angle = Mathf.Atan2(_mousePos.y, _mousePos.x) * Mathf.Rad2Deg;
        transform.localRotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}
