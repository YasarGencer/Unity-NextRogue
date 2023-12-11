using System.Threading.Tasks;
using UnityEngine;

public class B_MovementController : ANP_Movement
{
    public movementState MovementState { get; set; }
    public enum movementState {
        STAY,
        FOLLOW,
        KEEP_DISTANCE
    }
    public async override void Initialize(ANP_MainController mainController) {
        base.Initialize(mainController); 
        await Task.Delay(250);
        MovementState = movementState.FOLLOW;
    }
    protected override void UpdateRX(long obj) {
        //Debug.Log(_mainController.UseSkill.MoveTimer);
        if (_mainController.UseSkill.MoveTimer == false)
            return;
        //if to far away from the target or no target
        if (_mainController.Target.Target == null) {
            WalkSound(false);
            return;
        } else {
            if (_mainController.Distance(_mainController.Target.Target.transform) > _mainController.Stats.NoticeRange * 3) {
                MovementState = movementState.STAY;
                return;
            }
            if (_mainController.Distance(_mainController.Target.Target.transform) > _mainController.Stats.NoticeRange) {
                MovementState = movementState.FOLLOW;
                return;
            }
        } 

        switch (MovementState) {
            case movementState.STAY:
                Stay();
                break;
            case movementState.FOLLOW:
                Follow();
                break;
            case movementState.KEEP_DISTANCE:
                Keep_Distance();
                break;
            default:
                break;
        }
    }
    protected void Stay() {
        _mainController.Animator.SetBool("Walk", false);
        WalkSound(false);
    }
    protected void Follow() {        
        //returns if too close to the target
        if (_mainController.Distance(_mainController.Target.Target.transform) < _mainController.Stats.AttackRange * 0.8f) {
            WalkSound(false);
            _mainController.Animator.SetBool("Walk", false);
            return;
        }
        //if everything is optimal
        transform.position =
            Vector2.MoveTowards(transform.position, _mainController.Target.Target.transform.position, _mainController.Stats.SpeelHolder * Time.deltaTime);
        Rotate(_mainController.Target.Target.transform.position.x);
        WalkSound(true);
        _mainController.Animator.SetBool("Walk", true);
    }
    protected void Keep_Distance() { 
        if (_mainController.Distance(_mainController.Target.Target.transform) > _mainController.Stats.AttackRange) {
            //if everything is optimal
            WalkSound(true);
            _mainController.Animator.SetBool("Walk", true);

            transform.position =
                Vector2.MoveTowards(transform.position, _mainController.Target.Target.transform.position, _mainController.Stats.SpeelHolder * Time.deltaTime);
        } else if (_mainController.Distance(_mainController.Target.Target.transform) < _mainController.Stats.AttackRange - 1f) {
            WalkSound(true);
            _mainController.Animator.SetBool("Walk", true);

            Vector3 pos = _mainController.Target.Target.transform.position;
            Vector3 relativePos = new();
            relativePos.x = transform.position.x - pos.x;
            relativePos.y = transform.position.y - pos.y;
            Vector3 targetPos = transform.position + relativePos;
            transform.position =
                Vector2.MoveTowards(transform.position, targetPos, _mainController.Stats.SpeelHolder * .5f * Time.deltaTime);
        } else {
            //if it should stop moving
            WalkSound(false);
            _mainController.Animator.SetBool("Walk", false);
        }
        Rotate(_mainController.Target.Target.transform.position.x);
    }
}
