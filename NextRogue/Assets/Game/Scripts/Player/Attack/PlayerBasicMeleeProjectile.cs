using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBasicMeleeProjectile : APlayerBasicProjectile {
    bool _isHit;
    public override void Initialize(Vector3 mousePos) {
        base.Initialize(mousePos);
        StartCoroutine(IsHit());
    }
    IEnumerator IsHit() {
        _isHit = false;
        yield return new WaitForSeconds(.2f);
        _isHit = true;
        yield return new WaitForSeconds(.6f);
        _isHit = false;
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        if (!_isHit)
            return;
        switch (collision.gameObject.tag) {
            case "Enviroment":
                break;
            case "Enemy":
                break;
            default:
                break;
        }
    }
}
