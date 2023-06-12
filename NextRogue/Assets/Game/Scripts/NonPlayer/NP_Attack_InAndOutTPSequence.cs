using UnityEngine;
using System.Threading.Tasks;  

public class NP_Attack_InAndOutTPSequence : ANP_Attack {
    [SerializeField] GameObject _dashParticle;
    [SerializeField] AudioClip _dashIn, _dashOut;
    [SerializeField] LayerMask environmentLayer;
    protected override void UpdateRX(long obj) {
        base.UpdateRX(obj);
    }
    protected override void Attack() {
        if (MainManager.Instance.GameManager.GamePaused)
            return; 
        _mainController.Movement.Freeze();
        StartAttackingSequence();
    }
    async Task CheckHit() {
        await Task.Delay(250);
        if (_mainController.Target.Target == null) {
            EndSequence();
            return;
        }
        while (MainManager.Instance.GameManager.GamePaused) { 
            await Task.Delay(100);
        } 
        if (_mainController.Target.Target == null || _mainController == null) {
            EndSequence();
            return;
        }
        AudioManager.PlaySound(_mainController.Stats.AttackSound);
        if (_mainController.Distance(_mainController.Target.Target.transform) < _mainController.Stats.AttackRange / 4)
            _mainController.Target.Target.GetComponent<Health>().GetDamage(_mainController.Stats.AttackDamage, transform);
    }
    async void StartAttackingSequence() {
        if (_isAttacking)
            return;
        _isAttacking = true;
        _attackTime = _mainController.Stats.AttackSpeed;
        if (_mainController.Target.Target == null) {
            EndSequence();
            return;
        }
        //phase 1 - tp behind player
        Vector3 startPos = transform.position;
        Vector2 direction = _mainController.Target.Target.transform.position - transform.position;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, _mainController.Stats.AttackRange + 1, environmentLayer);
        if (hit.collider != null) {
            EndSequence();
            return;
        }
        Destroy(Instantiate(_dashParticle, transform.position, Quaternion.identity), 1f);
        AudioManager.PlaySound(_dashIn);
        Vector2 targetPos = (Vector2)(transform.position) + direction * 1.2f;
        transform.position = targetPos;
        //phase 2 - hit player
        _mainController.Movement.Rotate(_mainController.Target.Target.transform.position.x);
        _mainController.Animator.SetTrigger("attack");
        await CheckHit();
        //phase 3 - tp away from player
        await Task.Delay(250);   
        Destroy(Instantiate(_dashParticle, transform.position, Quaternion.identity), 1f);
        AudioManager.PlaySound(_dashOut);
        transform.position = startPos;
        //phase 4 - reset values
        EndSequence();
    }
    void EndSequence() { 
        _isAttacking = false;
        _mainController.Movement.UnFreeze();
    }  
}
