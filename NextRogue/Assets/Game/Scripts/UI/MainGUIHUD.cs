using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UI;

public class MainGUIHUD : MonoBehaviour
{
    public static MainGUIHUD Instance = null;
    P_MainController _mainController;

    [SerializeField]
    HUDHealthBar Health;

    public HUDSkillIcon[] spellIconList;
    public string[] spellKeys;

    [Header("SPELL DESCRIPTION")]
    public HUDSkillDescription Description;
    public void Awake() {
        Instance = this;
    }
    public void Initialize(P_MainController mainController) {
        _mainController = mainController;
        Description.Hide();
        Health.Initialize(_mainController);
        for (int i = 0; i < spellKeys.Length; i++) {
            spellIconList[i].SetKey(spellKeys[i]);
        }
    }
    public void SetHealth() {
        Health.SetHealth();
    }
    public void SetSlider(Slider slider, float max, float value) {
        slider.maxValue= max;
        slider.value= value;
    }
    public void SetSkillIcon(HUDSkillIcon[] list,int keyIndex, ASpell spell) {
        list[keyIndex].Initialize(spell, _mainController);
    }
}