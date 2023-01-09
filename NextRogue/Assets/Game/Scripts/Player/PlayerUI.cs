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
    public HUDSkillIcon[] basicIconList;
    public HUDSkillIcon[] spellIconList;

    public void Initialize(PlayerMainController mainController) {
        _mainController = mainController;
        SetSlider(HealthSlider, _mainController.Stats.MaxHealth, _mainController.Stats.Health);
        basicIconList[0].SetKey("l shift");
        basicIconList[1].SetKey("q");
        basicIconList[2].SetKey("l click");
        basicIconList[3].SetKey("r click");
        for (int i = 0; i < spellIconList.Length; i++) {
            spellIconList[i].SetKey((i+1).ToString());
        }
    }
    public void SetSlider(Slider slider, float max, float value) {
        slider.maxValue= max;
        slider.value= value;
    }
    public void SetIcon(HUDSkillIcon[] list,int keyIndex, ASpell spell) {
        list[keyIndex].SetIcon(spell);
    }
}