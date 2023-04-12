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
        _player = GameObject.FindObjectOfType<P_MainController>().transform;
        StartCoroutine(Animation());
    }
    protected override void Interact() { 
        MainManager.Instance.GameManager.LoadScene(2,2);
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
