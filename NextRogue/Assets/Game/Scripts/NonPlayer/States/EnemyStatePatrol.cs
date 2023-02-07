using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.InputSystem.XR;

[CreateAssetMenu(fileName = "EnemyPatrolState", menuName = "ScriptableObjects/EnemyStates/EnemyPatrolState")]
public class EnemyStatePatrol : AStates {
    List<Vector2> _patrolPoints= new List<Vector2>();

    public override void ActivateState(NonPlayerMainController mainController) {
        base.ActivateState(mainController);
        //CHECKS TARGET IN EVERY 2 SECS
        SetPatrolPoints();
        _mainController.StartCoroutine(CheckTarget());
    }
    public override void DeactivateState() {
        base.DeactivateState();
        _mainController.ChangeTarget(null);
    }
    public override void UpdateRX(long obj) {
    }
    public void SetPatrolPoints() {
        if (_mainController.State.Room == null)
            return;
        for (int i = 0; i < 4; i++) {
            _patrolPoints.Add(_mainController.State.Room.Floor.ElementAt(Random.Range(0, _mainController.State.Room.Floor.Count)));
        }
    }
    public void FollowPlayer() {
        List<GameObject> posibleTargets = new();
        foreach (var item in GameObject.FindGameObjectsWithTag("Summoned")) {
            posibleTargets.Add(item);
        }
        posibleTargets.Add(_mainController.Player.gameObject);
        _mainController.ChangeTarget(_mainController.Player.gameObject);
        float minDist = _mainController.CheckDistance(_mainController.AttackTarget.transform.position);
        foreach (var item in posibleTargets) {
            float dist = Vector2.Distance(item.transform.position, _mainController.gameObject.transform.position);
            if (minDist > dist) {
                _mainController.ChangeTarget(item);
                minDist = dist;
            }
        }
    }
    public void FollowPath() {
        _mainController.ChangeTarget(_patrolPoints[Random.Range(0, _patrolPoints.Count)]);
    }
    public void CheckClosest() {
        if (_mainController.CheckDistance(_mainController.Player.transform.position) < 5f)
            FollowPlayer();
        else
            FollowPath();
    }
    IEnumerator CheckTarget() {
        CheckClosest();
        yield return new WaitForSeconds(2f);
        _mainController.StartCoroutine(CheckTarget());
    }
}
