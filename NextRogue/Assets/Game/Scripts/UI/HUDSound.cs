using UnityEngine;
using UnityEngine.UI;

public class HUDSound : MonoBehaviour
{
    [SerializeField] Slider genereal, music, sfx, ui, environment;
    private void Start() {
        genereal.maxValue= 1;
        music.maxValue= 1;
        sfx.maxValue= 1;
        ui.maxValue= 1;
        environment.maxValue= 1;

        genereal.value = AudioManager.GetVolume(AudioManager.AudioVolume.general);
        music.value = AudioManager.GetVolume(AudioManager.AudioVolume.music);
        sfx.value = AudioManager.GetVolume(AudioManager.AudioVolume.sfx);
        ui.value = AudioManager.GetVolume(AudioManager.AudioVolume.ui);
        environment.value = AudioManager.GetVolume(AudioManager.AudioVolume.environment);
         

        genereal.onValueChanged.AddListener(delegate { ValueChange(genereal, AudioManager.AudioVolume.general); });
        music.onValueChanged.AddListener(delegate { ValueChange(music, AudioManager.AudioVolume.music); });
        sfx.onValueChanged.AddListener(delegate { ValueChange(sfx, AudioManager.AudioVolume.sfx); });
        ui.onValueChanged.AddListener(delegate { ValueChange(ui, AudioManager.AudioVolume.ui); });
        environment.onValueChanged.AddListener(delegate { ValueChange(environment, AudioManager.AudioVolume.environment); });

    }
    void ValueChange(Slider slider, AudioManager.AudioVolume audioType) { 
        AudioManager.SetVolume(audioType, slider.value);
    }
}
