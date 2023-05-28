using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDSound : MonoBehaviour
{
    [SerializeField] Slider genereal, music, sfx;
    private void Start() {
        genereal.maxValue= 1;
        music.maxValue= 1;
        sfx.maxValue= 1;

        genereal.value = AudioManager.GetVolume(AudioManager.AudioVolume.general);
        music.value = AudioManager.GetVolume(AudioManager.AudioVolume.music);
        sfx.value = AudioManager.GetVolume(AudioManager.AudioVolume.sfx);
         

        genereal.onValueChanged.AddListener(delegate { ValueChange(genereal, AudioManager.AudioVolume.general); });
        music.onValueChanged.AddListener(delegate { ValueChange(music, AudioManager.AudioVolume.music); });
        sfx.onValueChanged.AddListener(delegate { ValueChange(sfx, AudioManager.AudioVolume.sfx); });

    }
    void ValueChange(Slider slider, AudioManager.AudioVolume audioType) { 
        AudioManager.SetVolume(audioType, slider.value);
    }
}
