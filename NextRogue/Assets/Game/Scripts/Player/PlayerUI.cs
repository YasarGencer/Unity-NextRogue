using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    PlayerMainController _mainController;
    public Slider HealthSlider, SoulSlider;
    public Slider DashSlider, MeleeSlider, RangedSlider;
    public void Initialize(PlayerMainController mainController) {
        _mainController = mainController;
        SetSlider(HealthSlider, _mainController.Stats.MaxHealth, _mainController.Stats.Health);
    }
    public void SetSlider(Slider slider, float max, float value) {
        slider.maxValue= max;
        slider.value= value;
    }
}
