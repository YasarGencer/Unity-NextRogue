using System;
using System.Collections;
using UnityEngine;
using UniRx; 

public abstract class ANP_Movement : MonoBehaviour
{
    protected ANP_MainController _mainController;
    protected IDisposable _updateRX;
    protected Vector2 _patrolPosition;
    protected AudioSource _audioSource;
    public virtual void Initialize(ANP_MainController mainController) {
        _mainController = mainController;
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = _mainController.Stats.WalkSound != null? _mainController.Stats.WalkSound: null;
        UnFreeze();
        RegisterEvents();
        StartCoroutine(PatrolPos());
    }

    protected abstract void UpdateRX(long obj);
    IEnumerator PatrolPos() {
        _patrolPosition = new Vector2(transform.position.x, transform.position.y) + Direction2D.GetRandomEightDirection();
        yield return new WaitForSeconds(1f);
        StartCoroutine(PatrolPos());
    }

    public virtual IEnumerator FreezeMovement(float time) {
        Freeze();
        yield return new WaitForSeconds(time);
        UnFreeze();
    }
    public virtual IEnumerator SlowSpeed(float time, float percentage) {
        Slow(percentage);
        yield return new WaitForSeconds(time);
        UnSlow();
    }

    public virtual void Freeze() {
        _updateRX?.Dispose();
        WalkSound(false);
        if (_mainController != null && _mainController.Animator != null)
            _mainController?.Animator?.SetBool("Idle", true);
    }
    public virtual void UnFreeze() {
        if (MainManager.Instance.GameManager.GamePaused)
            return;
        _updateRX?.Dispose();
        _updateRX = Observable.EveryUpdate().TakeUntilDisable(this).Subscribe(UpdateRX);
        if (_mainController.Animator != null)
            _mainController.Animator.speed = 1;
    }
    protected virtual void Slow(float percentage) {
        _mainController.Stats.SpeelHolder = _mainController.Stats.Speed * (1 - percentage);
    }
    protected virtual void UnSlow() {
        _mainController.Stats.SpeelHolder = _mainController.Stats.Speed;
    }
    public void Rotate(float posX) {
        if (posX > transform.position.x + 0.1f)
            transform.localScale = new Vector3(1, 1, 1);
        else if (posX < transform.position.x - 0.1f)
            transform.localScale = new Vector3(-1, 1, 1);
    }
    public virtual void Die() {
        _updateRX?.Dispose();
    }
    // EVENTS 
    void RegisterEvents() { 
        MainManager.Instance.EventManager.onGamePause += OnGamePause;
        MainManager.Instance.EventManager.onGameUnPause += OnGameUnPause;
    }
    void OnGamePause() {
        Freeze(); 
    }
    void OnGameUnPause() { 
        UnFreeze();
    }
    protected void WalkSound(bool value) {
        if (_audioSource == null || _audioSource.clip == null)
            return;
        _audioSource.volume = AudioManager.GetVolume(AudioManager.AudioVolume.sfx);
        if(value == false) {
            _audioSource.Stop();
            return;
        } else {
            if(_audioSource.isPlaying == false)
                _audioSource.Play();
        }
    }
}
