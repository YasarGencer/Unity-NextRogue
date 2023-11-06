using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UniRx;

public class P_MenuMovement : MonoBehaviour
{
    bool _isInit;
    P_MainController _mainController;
    Animator _animator;
    AudioSource _audioSource;

    IDisposable _moveRX;
    public Animator Animator { get { return _animator; } }
    private Vector2 _direction;
    public Vector2 Direction { get { return _direction; } }

    public void Initialize(P_MainController mainController)
    {
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
        _direction = Vector2.zero;
    }
    public void MoveRX(long l)
    {
            transform.Translate(_direction * _mainController.Stats.Speed * Time.deltaTime);
    }
    
    void OnGamePause()
    {
        _moveRX?.Dispose();
        _direction = Vector2.zero;
    }
    void OnGameUnPause()
    {
        _moveRX?.Dispose();
        _moveRX = Observable.EveryUpdate().TakeUntilDisable(this).Subscribe(MoveRX);
        _direction = Vector2.zero;
    }
    
    IEnumerator StartFade(AudioSource audioSource, float duration, float targetVolume)
    {
        float currentTime = 0;
        float start = audioSource.volume;

        //var volumeDifference = Mathf.Abs(targetVolume - _audioSource.volume);
        //var adjustedDuration = volumeDifference * duration / MainManager.Instance.AudioManager.sfxVolume;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        yield break;
    }
    
}
