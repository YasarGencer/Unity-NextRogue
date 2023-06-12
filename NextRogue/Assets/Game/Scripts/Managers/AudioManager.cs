using System.Collections.Generic; 
using UnityEngine; 

public class AudioManager : MonoBehaviour {
    public MusicPlayer MusicPlayer { get; private set; }
    public List<AudioController> audioControllers = new();
    int index = 0;
    public static float GetVolume(AudioVolume volumeType) {
        var multiplier = volumeType == AudioVolume.general? 1.0f : GetVolume(AudioVolume.general);
        return PlayerPrefs.GetFloat(GetString(volumeType), volumeType == AudioVolume.general ? 1.0f: .5f) * multiplier;
    }
    static string GetString(AudioVolume volumeTpye) {
        switch (volumeTpye) {
            case AudioVolume.general:
                return "SOUND-GENERAL"; 
            case AudioVolume.music:
                return "SOUND-MUSIC";
            case AudioVolume.sfx:
                return "SOUND-SFX";
            case AudioVolume.ui:
                return "SOUND-UI";
            case AudioVolume.environment:
                return "SOUND-ENVIRONMENT";
        }
        return "a";
    }
    public static void SetVolume(AudioVolume volumeType, float value) { 
        PlayerPrefs.SetFloat(GetString(volumeType), value);
    }
    public enum AudioVolume {
        general,
        music,
        sfx,
        ui,
        environment
    }
    public static void PlaySound(AudioClip clip, Transform position = null, AudioManager.AudioVolume volumeType = AudioManager.AudioVolume.sfx, bool value = false) {
        if (clip == null)
            return;
        AudioManager manager = GameObject.FindObjectOfType<AudioManager>();
        if (manager.audioControllers.Count < 15) {
            manager.CreateNew(clip, position);
            manager.audioControllers[manager.index % manager.audioControllers.Count].Initialize(clip, volumeType, value);
            manager.index++;
        } else {
            manager.audioControllers[manager.index % manager.audioControllers.Count].Initialize(clip, volumeType, value);
            manager.index++;
        }
    }
    void CreateNew(AudioClip clip, Transform position = null) {
        GameObject soundObject = new GameObject("sound");
        soundObject.transform.position = position == null ? soundObject.transform.position : position.position;
        AudioController controller = soundObject.AddComponent<AudioController>();
        soundObject.transform.parent = transform;
        audioControllers.Add(controller);
    }
    public static void CreateAudioManager() {
        if (GameObject.FindObjectOfType<AudioManager>())
            return;
        GameObject audio = new GameObject("AudioManager");
        AudioManager auidoManager = audio.AddComponent<AudioManager>();
        auidoManager.MusicPlayer = audio.AddComponent<MusicPlayer>();

        DontDestroyOnLoad(audio);

        AudioClip[] clips = Resources.LoadAll<AudioClip>("Music/Hype"); 
        List<AudioClip> hype = new List<AudioClip>(clips);


        clips = Resources.LoadAll<AudioClip>("Music/Soft");
        List<AudioClip> soft = new List<AudioClip>(clips);

        auidoManager.MusicPlayer.Initialize(hype, soft);
    }
    public static MusicPlayer GetMusicPlayer() {
        AudioManager audioManager = GameObject.FindObjectOfType<AudioManager>();
        if (audioManager == null)
            return null;
        if (audioManager.MusicPlayer == null)
            return null;
        return audioManager.MusicPlayer;
    }
}
