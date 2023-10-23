using System;
using System.Collections;
using UniRx; 
using UnityEngine;

public class EscapePortal : AInteract {
    Transform _player;
    [SerializeField]
    float _animationRange;

    [SerializeField] bool canReverseEnter = false;
    [SerializeField] float range;
    IDisposable updateRX; 

    protected override void OnStart() {
        base.OnStart();

        if (canReverseEnter) {
            _canUse = false;
            updateRX = Observable.EveryUpdate().TakeUntilDisable(this).Subscribe(UpdateRX);
        } else {
            _canUse = true;
        }
        StartCoroutine(Animation());
    }
    public void InteractPublic() {
        Interact();
    }
    protected override void Interact() {
        if (_canUse == false)
            return;
        base.Interact();
        LevelSettings.SetIfTutorial();
        MainManager.Instance.Player.GetComponentInChildren<P_MainController>().Stats.SetTutorial(1);
        //MainManager.Instance.Player.GetComponentInChildren<P_MainController>().Spells.DeleteMainSpells();
        MainManager.Instance.StartGame(false);
    }
    private void OnDrawGizmos() {
        UnityEngine.Gizmos.DrawWireSphere(transform.position, _animationRange);
    }
    IEnumerator Animation() {
        if(_player == null) {
            _player = MainManager.Instance.Player.GetChild(0);
        }
        if (Vector2.Distance(_player.position, transform.position) <= _animationRange && _canUse)
            _animator.SetBool("Open",true);
        else
            _animator.SetBool("Open", false);
        yield return new WaitForSeconds(.25f);
        StartCoroutine(Animation());
    }
    void UpdateRX(long obj) {
        if (Vector3.Distance(_player.transform.position, this.transform.position) >= range) { 
            _canUse = true;
            updateRX?.Dispose();
        }
    } 
}
