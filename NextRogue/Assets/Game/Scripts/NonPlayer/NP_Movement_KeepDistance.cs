using UnityEngine;

public class NP_Movement_KeepDistance : ANP_Movement {
    protected override void UpdateRX(long obj) {
        //returns if to no target
        if (_mainController.Target.Target == null)
            return;
        //if to far away from the target
        if (_mainController.Distance(_mainController.Target.Target.transform) > _mainController.Stats.NoticeRange) {
            transform.position =
                Vector2.MoveTowards(transform.position, _patrolPosition, _mainController.Stats.Speed * Time.deltaTime);
            Rotate(_patrolPosition.x);
            return;
        }
        if (_mainController.Distance(_mainController.Target.Target.transform) > _mainController.Stats.AttackRange) {
            //if everything is optimal
            transform.position =
                Vector2.MoveTowards(transform.position, _mainController.Target.Target.transform.position, _mainController.Stats.Speed * Time.deltaTime);
        } else if (_mainController.Distance(_mainController.Target.Target.transform) < _mainController.Stats.AttackRange - .5f) {
            Vector3 pos = _mainController.Target.Target.transform.position;
            Vector3 relativePos = new();
            relativePos.x = transform.position.x - pos.x;
            relativePos.y = transform.position.y - pos.y;
            Vector3 targetPos = transform.position + relativePos;
            transform.position =
                Vector2.MoveTowards(transform.position, targetPos, _mainController.Stats.Speed * Time.deltaTime);
        }
        Rotate(_mainController.Target.Target.transform.position.x);
    }
}
