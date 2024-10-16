using UnityEngine;

public class NP_Movement_Follow : ANP_Movement {
    protected override void UpdateRX(long obj) {
        //if to far away from the target or no target
        if (_mainController.Target.Target == null) {
            WalkSound(false);
            return;
        } else {
            WalkSound(false);
            if (_mainController.Distance(_mainController.Target.Target.transform) > _mainController.Stats.NoticeRange * 3)
                return;
            if (_mainController.Distance(_mainController.Target.Target.transform) > _mainController.Stats.NoticeRange) {
                transform.position =
                    Vector2.MoveTowards(transform.position, _patrolPosition, _mainController.Stats.SpeelHolder * Time.deltaTime);
                Rotate(_patrolPosition.x);
                return;
            }
        }
        //returns if too close to the target
        if (_mainController.Distance(_mainController.Target.Target.transform) < _mainController.Stats.AttackRange * 0.8f) {
            WalkSound(false);
            return;
        }
        //if everything is optimal
        transform.position = 
            Vector2.MoveTowards(transform.position, _mainController.Target.Target.transform.position, _mainController.Stats.SpeelHolder * Time.deltaTime);
        Rotate(_mainController.Target.Target.transform.position.x);
        WalkSound(true);
    }
    public override void Die() {
        base.Die();
    }
}
