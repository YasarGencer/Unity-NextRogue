using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "SummonMovementState", menuName = "ScriptableObjects/SummonStates/SummonMovementState")]
public class SummonStateMovement : AStates {
    public override void ActivateState(NonPlayerMainController mainController) {
        base.ActivateState(mainController);
        //CHANGES TARGET IN EVERY 2 SECS
        _mainController.StartCoroutine(CheckTarget());
    }
    public override void DeactivateState() {
        base.DeactivateState();
    }
    public override void UpdateRX(long obj) {
        //NO TARGET
        if (_mainController.Target == null)
            CheckClosest();
        float dist = Vector2.Distance(_mainController.transform.position, _mainController.Target.transform.position);
        //TARGET IS AN ENEMY AN TO CLOSE
        if (_mainController.Target != _mainController.Player.gameObject && dist < _mainController.Stats.Range - 1)
            return;
        //TARGET IS A PLAYER AN TO CLOSE
        if (_mainController.Target == _mainController.Player.gameObject && dist < _mainController.Player.Stats.MinSummonControlRange)
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
        //CHECK IF PLAYER IS TOO FAR AWAY
        if (Vector2.Distance(_mainController.transform.position, _mainController.Player.transform.position) > _mainController.Player.Stats.MaxSummonControlRange) {
            _mainController.ChangeTarget(_mainController.Player.gameObject);
            return;
        }
        //CHECK IF ENEMIES ARE TOO FAR AWAY
        if (_mainController.Target != null)
            if (_mainController.Target != _mainController.Player && Vector2.Distance(_mainController.transform.position, _mainController.Target.transform.position) > _mainController.Stats.EnemyRange) {
                _mainController.ChangeTarget(_mainController.Player.gameObject);
                return;
            }
        //IF TARGET IS NULL CHECKS 10 METER ROUND FOR TARGET IF NONE AVAILABLE SETS PLAYER AS A TARGET
        float minDist = _mainController.Target == null ? _mainController.Stats.EnemyRange : Vector2.Distance(_mainController.transform.position, _mainController.Target.transform.position);
        foreach (var item in GameObject.FindGameObjectsWithTag("Enemy")) {
            float dist = Vector2.Distance(item.transform.position, _mainController.gameObject.transform.position);
            if (minDist > dist) {
                _mainController.ChangeTarget(item);
                minDist = dist;
            }
        }
        //IF THERE IS NO ENEMY
        if (_mainController.Target == null) _mainController.ChangeTarget(_mainController.Player.gameObject);
    }
    IEnumerator CheckTarget() {
        CheckClosest();
        yield return new WaitForSeconds(2f);
        _mainController.StartCoroutine(CheckTarget());
    }
}
