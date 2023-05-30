using System;
using System.Collections;
using System.Collections.Generic; 
using UnityEngine; 

public class AudioController : MonoBehaviour
{
    AudioSource _source;
    public void Initialize(AudioClip clip, bool value = false) {
        if (value) {
            MainManager.Instance.EventManager.onGamePause += OnGamePause;
            MainManager.Instance.EventManager.onGameUnPause += OnGameUnPause;
        }

        _source = gameObject.AddComponent<AudioSource>();
        _source.volume = AudioManager.GetVolume(AudioManager.AudioVolume.sfx);
        _source.clip = clip;
        _source.Play(); 
    }

    private void OnGameUnPause() {
        _source.UnPause();
    }

    private void OnGamePause() {
        _source.Pause();
    }
}
