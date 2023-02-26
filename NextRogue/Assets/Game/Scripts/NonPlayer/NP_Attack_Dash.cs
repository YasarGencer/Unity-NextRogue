using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class NP_Attack_Dash : ANP_Attack {
    IDisposable _checkRX;
    List<GameObject> _hitList = new();
    protected override void UpdateRX(long obj) {
        if (_mainController.Target.Target == null)
            return;
        if (_mainController.Distance(_mainController.Target.Target.transform) < _mainController.Stats.AttackRange)
            Attack();
    }
    private void Attack() {
        _checkRX?.Dispose();
        StartCoroutine(AttackLimiter());
        StartCoroutine(_mainController.Movement.FreezeMovement(1));
        StartCoroutine(Dash());
        _mainController.Animator.SetTrigger("Charge");
    }
    IEnumerator Dash() {
        if (_mainController.Target != null) {
            yield return new WaitForSeconds(1f);

            _mainController.Animator.SetTrigger("Dash");

            ClearHitList();
            _checkRX = Observable.EveryUpdate().Subscribe(CheckCollision);
            
            Vector3 difference = _mainController.Target.Target.transform.position - transform.position;
            difference = difference.normalized;
            float angle = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            Vector3 dir = Quaternion.AngleAxis(angle, Vector3.forward) * Vector3.right;

            _mainController.Rb.AddForce(dir * 30000 * Time.fixedDeltaTime);

            yield return new WaitForSeconds(.6f);
            
            _checkRX?.Dispose();
            
            _mainController.Animator.SetTrigger("Walk");
            _mainController.Rb.velocity = Vector2.zero;
        }
    }
    void CheckCollision(long obj) {
        Collider2D[] collisions = Physics2D.OverlapCircleAll(gameObject.transform.GetChild(0).position, GetComponent<CapsuleCollider2D>().size.x);
        foreach (var item in collisions)
            if (item.GetComponent<Health>() as Health != null && item.tag != transform.tag && !_hitList.Contains(item.gameObject)){
                    _hitList.Add(item.gameObject);
                    item.GetComponent<Health>().GetDamage(_mainController.Stats.AttackDamage, this.transform);
                }
    }
    void ClearHitList() {
        _hitList = new();
    }
}
