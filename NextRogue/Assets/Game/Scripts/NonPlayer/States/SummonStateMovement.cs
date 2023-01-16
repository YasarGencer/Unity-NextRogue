using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "SummonMovementState", menuName = "ScriptableObjects/SummonStates/SummonMovementState")]
public class SummonStateMovement : AStates {

    public override void ActivateState(NonPlayerMainController mainController) {
        base.ActivateState(mainController);
        _mainController.StartCoroutine(CheckTarget());
    }
    public override void DeactivateState() {
        base.DeactivateState();
    }
    public override void UpdateRX(long obj) {
        if (_mainController.Target == null)
            CheckClosest();
        if (Vector2.Distance(_mainController.transform.position, _mainController.Target.transform.position) < _mainController.Stats.Range - .5f)
            return;
        Follow();
    }
    public void Follow() {
        _mainController.gameObject.transform.position = Vector3.MoveTowards(_mainController.gameObject.transform.position, _mainController.Target.transform.position, _mainController.Stats.Speed * Time.deltaTime);
        float dist = _mainController.gameObject.transform.position.x - _mainController.Target.transform.position.x;
        int scale = 1;
        if (dist > .5f)
            scale = -1;
        else if (dist < -.5f)
            scale = 1;
        _mainController.gameObject.transform.localScale = new Vector3(scale, 1, 1);
    }
    public void CheckClosest() {
        float minDist = _mainController.Target == null ? 10000 : Vector2.Distance(_mainController.transform.position, _mainController.gameObject.transform.position);
        GameObject[] summons = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var item in summons) {
            float dist = Vector2.Distance(item.transform.position, _mainController.gameObject.transform.position);
            if (minDist > dist) {
                _mainController.ChangeTarget(item);
                minDist = dist;
            }
        }
    }
    IEnumerator CheckTarget() {
        CheckClosest();
        yield return new WaitForSeconds(2f);
        _mainController.StartCoroutine(CheckTarget());
    }
}
