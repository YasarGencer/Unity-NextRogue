using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VSCodeEditor;
using UniRx;
using UnityEngine.Rendering;

public abstract class ANP_Movement : MonoBehaviour
{
    protected NP_MainController _mainController;
    protected IDisposable _updateRX;
    protected float _speedHolder;
    protected Vector2 _patrolPosition;
    public virtual void Initialize(NP_MainController mainController) {
        _mainController = mainController;
        _speedHolder = mainController.Stats.Speed;
        UnFreeze();
        StartCoroutine(PatrolPos());
    }

    protected abstract void UpdateRX(long obj);
    IEnumerator PatrolPos() {
        _patrolPosition = new Vector2(transform.position.x, transform.position.y) + Direction2D.GetRandomEightDirection();
        yield return new WaitForSeconds(1f);
        StartCoroutine(PatrolPos());
    }



    public virtual IEnumerator FreezeTime(float time) {
        Freeze();
        yield return new WaitForSeconds(time);
        UnFreeze();
    }
    public virtual IEnumerator SlowSpeed(float time, float percentage) {
        Slow(percentage);
        yield return new WaitForSeconds(time);
        UnSlow();
    }

    protected virtual void Freeze() {
        _updateRX?.Dispose();
    }
    protected virtual void UnFreeze() {
        _updateRX?.Dispose();
        _updateRX = Observable.EveryUpdate().Subscribe(UpdateRX);
    }
    protected virtual void Slow(float percentage) {
        _mainController.Stats.Speed *= 1 - percentage;
    }
    protected virtual void UnSlow() {
        _mainController.Stats.Speed = _speedHolder;
    }
    protected void Rotate(float posX) {
        if (posX > transform.position.x + 0.1f)
            transform.localScale = new Vector3(1, 1, 1);
        else if (posX < transform.position.x - 0.1f)
            transform.localScale = new Vector3(-1, 1, 1);
    }
}
