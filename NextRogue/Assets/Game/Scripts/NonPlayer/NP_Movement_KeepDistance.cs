using UnityEngine;

public class NP_Movement_KeepDistance : ANP_Movement {
    protected override void UpdateRX(long obj) {
        //returns if to no target
        if (_mainController.Target.Target == null) {
            WalkSound(false);
            return;
        }
        //if to far away from the target
        if (_mainController.Distance(_mainController.Target.Target.transform) > _mainController.Stats.NoticeRange) {
            transform.position =
                Vector2.MoveTowards(transform.position, _patrolPosition, _mainController.Stats.SpeelHolder * Time.deltaTime);
            Rotate(_patrolPosition.x);
            WalkSound(false);
            return;
        }
        if (_mainController.Distance(_mainController.Target.Target.transform) > _mainController.Stats.AttackRange) {
            //if everything is optimal
            WalkSound(true);
            _mainController.Animator.SetBool("Idle", false);

            transform.position =
                Vector2.MoveTowards(transform.position, _mainController.Target.Target.transform.position, _mainController.Stats.SpeelHolder * Time.deltaTime);
        } else if (_mainController.Distance(_mainController.Target.Target.transform) < _mainController.Stats.AttackRange - 1f) {
            WalkSound(true);
            _mainController.Animator.SetBool("Idle", false);

            Vector3 pos = _mainController.Target.Target.transform.position;
            Vector3 relativePos = new();
            relativePos.x = transform.position.x - pos.x;
            relativePos.y = transform.position.y - pos.y;
            Vector3 targetPos = transform.position + relativePos;
            transform.position =
                Vector2.MoveTowards(transform.position, targetPos, _mainController.Stats.SpeelHolder * .5f * Time.deltaTime);
        }
        else {
            //if it should stop moving
            WalkSound(false);
            _mainController.Animator.SetBool("Idle", true);
        }
        Rotate(_mainController.Target.Target.transform.position.x);
    }
}
