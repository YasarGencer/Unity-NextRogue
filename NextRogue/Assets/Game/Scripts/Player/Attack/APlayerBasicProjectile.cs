using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class APlayerBasicProjectile : MonoBehaviour
{
    Vector3 _mousePos;
    [SerializeField] GameObject _main;
    public virtual void Initialize(Vector3 mousePos) {
        Destroy(_main, 4f);

        _mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        SetRotation(_main.transform);
    }
    void SetRotation(Transform target) {
        _mousePos.z = 0f;

        _mousePos.x -= transform.position.x;
        _mousePos.y -= transform.position.y;

        float angle = Mathf.Atan2(_mousePos.y, _mousePos.x) * Mathf.Rad2Deg;
        target.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}
