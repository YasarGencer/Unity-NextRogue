using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public abstract class AInteract : MonoBehaviour {

    protected bool _isInteractable; 
    [SerializeField]
    protected GameObject _info;
    protected Animator _animator;

    private void Start() => OnStart();
    protected virtual void OnStart() {
        MainManager.Instance.EventManager.onInteract += CheckInteraction;
        _animator = GetComponent<Animator>();
    }
    protected void CheckInteraction() {
        if (!MainManager.Instance.GameManager.GamePaused)
            if (_isInteractable)
                Interact();
    }
    protected virtual void Interact() {
        Info(false);
    }

    private void OnDrawGizmos() { 
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.CompareTag("Player"))
            Info(true);
    }
    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Player"))
            Info(false);
    }
    protected void Info(bool value) {
        _isInteractable = value;
        _info.SetActive(value);
    } 
}
