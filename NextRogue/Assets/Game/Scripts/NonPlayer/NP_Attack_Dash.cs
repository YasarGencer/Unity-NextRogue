using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class NP_Attack_Dash : ANP_Attack {
    IDisposable _checkRX;
    List<GameObject> _hitList = new();
    bool isDashing = false;
    protected override void UpdateRX(long obj) {
        base.UpdateRX(obj);
    }
    protected override void Attack() {
        if (MainManager.Instance.GameManager.GamePaused)
            return;
        _checkRX?.Dispose();
        _attackTime = _mainController.Stats.AttackSpeed;
        StartCoroutine(_mainController.Movement.FreezeMovement(1));
        StartCoroutine(Dash());
    }
    IEnumerator Dash() {
        if (_mainController.Target != null) {
            _mainController.Animator.SetTrigger("Charge"); 

            yield return new WaitForSeconds(1f);
            if (!MainManager.Instance.GameManager.GamePaused) {
                isDashing = true;
                _mainController.Animator.SetTrigger("Dash");

                ClearHitList();
                _checkRX = Observable.EveryUpdate().Subscribe(CheckCollision);

                Vector3 difference = _mainController.Target.Target.transform.position - transform.position;
                difference = difference.normalized;
                float angle = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
                Vector3 dir = Quaternion.AngleAxis(angle, Vector3.forward) * Vector3.right;

                _mainController.Rb.AddForce(dir * _mainController.Stats.ProjectileSpeed * Time.fixedDeltaTime);
                
            }
            yield return new WaitForSeconds(.6f);

            if (!MainManager.Instance.GameManager.GamePaused) {
                _checkRX?.Dispose();

                _mainController.Animator.SetTrigger("Walk");
                _mainController.Rb.velocity = Vector2.zero;
                isDashing = false;
            }
        }
    }
    IEnumerator DashUnPause() {
        if (!MainManager.Instance.GameManager.GamePaused) {
            _mainController.Animator.SetTrigger("Dash");

            ClearHitList();
            if (gameObject) {
                _checkRX?.Dispose();
                _checkRX = Observable.EveryUpdate().Subscribe(CheckCollision);

                Vector3 difference = _mainController.Target.Target.transform.position - transform.position;
                difference = difference.normalized;
                float angle = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
                Vector3 dir = Quaternion.AngleAxis(angle, Vector3.forward) * Vector3.right;

                _mainController.Rb.AddForce(dir * 30000 * Time.fixedDeltaTime);
            }
        }
        yield return new WaitForSeconds(.6f);

        _checkRX?.Dispose();

        _mainController.Animator.SetTrigger("Walk");
        _mainController.Rb.velocity = Vector2.zero; 
        isDashing = false;
    }
    void CheckCollision(long obj) {
        Collider2D[] collisions = Physics2D.OverlapCircleAll(gameObject.transform.GetChild(0).position, GetComponent<CapsuleCollider2D>().size.x);
        foreach (var item in collisions) {
            if (item)
                if (item?.GetComponent<Health>() as Health != null && item.tag != transform.tag && !_hitList.Contains(item?.gameObject)) {
                    _hitList.Add(item?.gameObject);
                    item?.GetComponent<Health>().GetDamage(_mainController.Stats.AttackDamage, this.transform);
                }
        }
    }
    void ClearHitList() {
        _hitList.Clear();
    }
    public override void Die() {
        base.Die();
        StopAllCoroutines();
        _hitList.Clear();
        _checkRX?.Dispose();
    }
    //EVENT
    protected override void OnGamePause() {
        base.OnGamePause();
        if(_mainController)
            if(_mainController.Rb)
                _mainController.Rb.velocity = Vector2.zero;
        _checkRX?.Dispose();
    }
    protected override void OnGameUnPause() {
        base.OnGameUnPause();
        if (_mainController)
            if (isDashing)
                StartCoroutine(DashUnPause());
    }
}
