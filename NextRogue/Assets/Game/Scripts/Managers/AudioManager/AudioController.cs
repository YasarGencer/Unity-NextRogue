using UnityEngine; 

public class AudioController : MonoBehaviour
{
    AudioSource _source;
    public void Initialize(AudioClip clip, AudioManager.AudioVolume volumeType = AudioManager.AudioVolume.sfx, bool value = false) {
        if (value) {
            MainManager.Instance.EventManager.onGamePause += OnGamePause;
            MainManager.Instance.EventManager.onGameUnPause += OnGameUnPause;
        }
        if(_source == null)
            _source = gameObject.AddComponent<AudioSource>();
        _source.volume = AudioManager.GetVolume(volumeType);
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
