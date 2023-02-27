using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;

public class MainGUIHUD : MonoBehaviour
{
    public static MainGUIHUD Instance = null;
    P_MainController _mainController;
    public Slider HealthSlider, SoulSlider;
    public HUDSkillIcon[] spellIconList;
    public string[] spellKeys;
    [Header("SPELL DESCRIPTION")]
    public HUDSkillDescription Description;
    public void Awake() {
        Instance = this;
    }
    public void Initialize(P_MainController mainController) {
        _mainController = mainController;

        SetSlider(HealthSlider, _mainController.Stats.MaxHealth, _mainController.Stats.Health);

        Description.Hide();
        for (int i = 0; i < spellKeys.Length; i++) {
            spellIconList[i].SetKey(spellKeys[i]);
        }
    }
    public void SetSlider(Slider slider, float max, float value) {
        slider.maxValue= max;
        slider.value= value;
    }
    public void SetSkillIcon(HUDSkillIcon[] list,int keyIndex, ASpell spell) {
        list[keyIndex].Initialize(spell, _mainController);
    }
}