using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    PlayerMainController _mainController;
    public Slider HealthSlider, SoulSlider;
    [System.Serializable]
    public struct UIIcons {
        public Slider Slider;
        public TextMeshProUGUI Text;
        public Image[] Icon;
    }
    public UIIcons[] basicIconList;
    public UIIcons[] spellIconList;

    public void Initialize(PlayerMainController mainController) {
        _mainController = mainController;
        SetSlider(HealthSlider, _mainController.Stats.MaxHealth, _mainController.Stats.Health);
    }
    public void SetSlider(Slider slider, float max, float value) {
        slider.maxValue= max;
        slider.value= value;
    }
    public void SetIcon(UIIcons[] list,int keyIndex, ASpell spell) {
        foreach (var item in list[keyIndex].Icon) {
            item.sprite= spell.Icon;
        }
        list[keyIndex].Text.text = spell.Name;
    }
}