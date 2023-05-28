using UnityEngine;
using UniRx;
using System; 
using System.Collections.Generic;
using System.Collections;

public class P_Movement : MonoBehaviour {
    bool _isInit;
    P_MainController _mainController;
    Animator _animator;
    AudioSource _audioSource;

    IDisposable _moveRX;
    public Animator Animator { get { return _animator; } }
    private Vector2 _direction;
    public Vector2 Direction { get { return _direction; } }

    public void Initialize(P_MainController mainController) {
        if (_isInit)
            return;
        _isInit = true;

        MainManager.Instance.EventManager.onGamePause += OnGamePause;
        MainManager.Instance.EventManager.onGameUnPause += OnGameUnPause;
        _mainController = mainController;

        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = _mainController.Stats.WalkSound;
        _audioSource.volume = 0;
        _audioSource.Play();

        _animator = _mainController.Animator;
    }
    public void MoveRX(long l) {
        if (_mainController.canPlay)
            transform.Translate(_direction * _mainController.Stats.Speed * Time.deltaTime);
    }
    public void SetDirection(Vector2 direction) {
        _direction = direction;

        //set sprite
        _animator.SetFloat("moveDir", _direction.magnitude);

        if (_direction.x > 0)
            this.transform.localScale = new Vector2(1, 1);
        else if (_direction.x < 0)
            this.transform.localScale = new Vector2(-1, 1);

        //disposable movement
        _moveRX?.Dispose();
        if (_direction != Vector2.zero)
            _moveRX = Observable.EveryUpdate().TakeUntilDisable(this).Subscribe(MoveRX);
        WalkSound(_direction);
    }
    void OnGamePause() {
        _moveRX?.Dispose();
    }
    void OnGameUnPause() {
        _moveRX?.Dispose();
        _moveRX = Observable.EveryUpdate().TakeUntilDisable(this).Subscribe(MoveRX);
    }
    protected void WalkSound(Vector2 direction) {
        if (_audioSource.clip == null)
            return;
        if (direction == Vector2.zero || _mainController.canPlay == false) {
            StartCoroutine(StartFade(_audioSource, .25f, 0));
            return;
        }
        StartCoroutine(StartFade(_audioSource, .25f, AudioManager.GetVolume(AudioManager.AudioVolume.sfx)));
    }
    IEnumerator StartFade(AudioSource audioSource, float duration, float targetVolume) {
        float currentTime = 0;
        float start = audioSource.volume;
         
        //var volumeDifference = Mathf.Abs(targetVolume - _audioSource.volume);
        //var adjustedDuration = volumeDifference * duration / MainManager.Instance.AudioManager.sfxVolume;

        while (currentTime < duration) {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        yield break;
    }

}
