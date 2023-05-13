using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EscapePortal : AInteract {
    Transform _player;
    [SerializeField]
    float _animationRange;
    protected override void OnStart() {
        base.OnStart();
        _player = MainManager.Instance.Player.GetChild(0);
        StartCoroutine(Animation());
    }
    protected override void Interact() { 
        base.Interact();
        LevelSettings.SetIfTutorial();
        MainManager.Instance.Player.GetComponentInChildren<P_MainController>().Stats.SetTutorial(1);
        MainManager.Instance.Player.GetComponentInChildren<P_MainController>().Spells.DeleteMainSpells();
        MainManager.Instance.StartGame();
    }
    private void OnDrawGizmos() {
        UnityEngine.Gizmos.DrawWireSphere(transform.position, _animationRange);
    }
    IEnumerator Animation() {
        if (Vector2.Distance(_player.position, transform.position) <= _animationRange)
            _animator.SetBool("Open",true);
        else
            _animator.SetBool("Open", false);
        yield return new WaitForSeconds(.25f);
        StartCoroutine(Animation());
    }
}
