using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

[CreateAssetMenu(fileName = "SummonMovementState", menuName = "ScriptableObjects/SummonStates/SummonMovementState")]
public class SummonStateMovement : AStates {
    float timer;
    public override void ActivateState(NonPlayerMainController mainController) {
        base.ActivateState(mainController);
        //CHANGES TARGET IN EVERY 2 SECS
        _mainController.StartCoroutine(CheckTarget());
        timer = 0;
    }
    public override void DeactivateState() {
        base.DeactivateState();
    }
    public override void UpdateRX(long obj) {
        timer += Time.deltaTime;
        if (timer > _mainController.Stats.LifeSpan)
            _mainController.Health.Die();
        //NO TARGET
        if (_mainController.AttackTarget == null)
            CheckClosest();
        float dist = Vector2.Distance(_mainController.transform.position, _mainController.AttackTarget.transform.position);
        //TARGET IS AN ENEMY AN TO CLOSE
        if (_mainController.AttackTarget != _mainController.Player.gameObject && dist < _mainController.Stats.Range - .3f)
            return;
        //TARGET IS A PLAYER AN TO CLOSE
        if (_mainController.AttackTarget == _mainController.Player.gameObject && dist < _mainController.Player.Stats.MinSummonControlRange)
            return;

        Follow();
    }
    public void Follow() {
        _mainController.gameObject.transform.position = Vector3.MoveTowards(_mainController.gameObject.transform.position, _mainController.AttackTarget.transform.position, _mainController.Stats.Speed * Time.deltaTime);
        float dist = _mainController.gameObject.transform.position.x - _mainController.AttackTarget.transform.position.x;
        int scale = 1;
        if (dist > .5f)
            scale = -1;
        else if (dist < -.5f)
            scale = 1;
        _mainController.gameObject.transform.localScale = new Vector3(scale, 1, 1);
    }
    public void CheckClosest() {
        //CHECK IF PLAYER IS TOO FAR AWAY
        if (_mainController.CheckDistance(_mainController.Player.transform.position) > _mainController.Player.Stats.MaxSummonControlRange) {
            _mainController.ChangeTarget(_mainController.Player.gameObject);
            return;
        }
        //CHECK IF ENEMIES ARE TOO FAR AWAY
        if (_mainController.AttackTarget != null)
            if (_mainController.AttackTarget != _mainController.Player && Vector2.Distance(_mainController.transform.position, _mainController.AttackTarget.transform.position) > _mainController.Stats.EnemyRange) {
                _mainController.ChangeTarget(_mainController.Player.gameObject);
                return;
            }
        //IF TARGET IS NULL CHECKS IN RANGE FOR TARGET IF NONE AVAILABLE SETS PLAYER AS A TARGET
        float minDist = _mainController.AttackTarget == null ? _mainController.Stats.EnemyRange : Vector2.Distance(_mainController.transform.position, _mainController.AttackTarget.transform.position);
        foreach (var item in GameObject.FindGameObjectsWithTag("Enemy")) {
            float dist = _mainController.CheckDistance(item.transform.position);
            if (minDist > dist) {
                _mainController.ChangeTarget(item);
                minDist = dist;
            }
        }
        //IF THERE IS NO ENEMY
        if (_mainController.AttackTarget == null) _mainController.ChangeTarget(_mainController.Player.gameObject);
    }
    IEnumerator CheckTarget() {
        CheckClosest();
        yield return new WaitForSeconds(2f);
        _mainController.StartCoroutine(CheckTarget());
    }
}
