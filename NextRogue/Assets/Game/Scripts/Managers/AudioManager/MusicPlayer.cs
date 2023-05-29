using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour {
    public List<List<AudioClip>> musicLists = new List<List<AudioClip>>();

    private bool isInit = false;

    AudioSource audioSource;
    float activeVolume;

    public float fadeDuration = 3f;
    private Coroutine fadeOutCoroutine;
    private Coroutine fadeInCoroutine;

    private int currentListIndex = 0;
    private int currentTrackIndex = 0;

    public void Initialize(List<AudioClip> hype, List<AudioClip> soft) {
        musicLists.Add(hype);
        musicLists.Add(soft);
        audioSource = gameObject.AddComponent<AudioSource>();
        activeVolume = 0; 
        isInit = true;
    }
    private void Update() {
        if (isInit == false)
            return;
        audioSource.volume = activeVolume * AudioManager.GetVolume(AudioManager.AudioVolume.music);
        if (audioSource.isPlaying)
            return;
        ChangeTrack();
    }
    public void Play() {
        List<AudioClip> currentMusicList = musicLists[currentListIndex];
        audioSource.clip = currentMusicList[currentTrackIndex];
        audioSource.Play();
        Debug.Log(audioSource.clip.name);
        FadeIn();
        isInit = true;
    }
    public void SwitchMusicList(int listIndex) {
        if (listIndex >= 0 && listIndex < musicLists.Count) {
            if (listIndex != currentListIndex) {
                currentListIndex = listIndex; 
                ChangeTrack();
            }
        }
    }
    public void ChangeTrack() {
        isInit = false;
        List<AudioClip> currentMusicList = musicLists[currentListIndex];
        var random = Random.Range(1, currentMusicList.Count - 1);
        currentTrackIndex = (currentTrackIndex + random) % currentMusicList.Count;
        FadeOutAndPlayNext();
    }
    private void FadeOutAndPlayNext() {
        if (fadeOutCoroutine != null) {
            StopCoroutine(fadeOutCoroutine);
        }
        fadeOutCoroutine = StartCoroutine(FadeOutCoroutine(() => {
            Play();
        }));
    }
    private void FadeIn() {
        if (fadeInCoroutine != null) {
            StopCoroutine(fadeInCoroutine);
        }
        fadeInCoroutine = StartCoroutine(FadeInCoroutine());
    }
    private IEnumerator FadeOutCoroutine(System.Action callback) {
        float elapsedTime = 0f;
        float startVolume = activeVolume;

        while (elapsedTime < fadeDuration) {
            elapsedTime += Time.deltaTime;
            activeVolume = Mathf.Lerp(startVolume, 0f, elapsedTime / fadeDuration);
            yield return null;
        }

        activeVolume = 0f;
        callback?.Invoke();
    }
    private IEnumerator FadeInCoroutine() {
        float elapsedTime = 0f;
        float targetVolume = 1;

        while (elapsedTime < fadeDuration) {
            elapsedTime += Time.deltaTime;
            activeVolume = Mathf.Lerp(0f, targetVolume, elapsedTime / fadeDuration);
            yield return null;
        }

        activeVolume = targetVolume;
    }
}
