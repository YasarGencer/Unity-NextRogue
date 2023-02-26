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
    public Description SpellDescription;
    [System.Serializable]
    public struct Description {
        public GameObject GameObject;
        public TextMeshProUGUI Name;
        public TextMeshProUGUI Desc;
        public TextMeshProUGUI Cooldown;
        public Image Icon;
    }
    public void Awake() {
        Instance = this;
    }
    public void Initialize(P_MainController mainController) {
        _mainController = mainController;
        SpellDescription.GameObject.SetActive(false);
        SetSlider(HealthSlider, _mainController.Stats.MaxHealth, _mainController.Stats.Health);
        SetKeys();
    }
    public void SetSlider(Slider slider, float max, float value) {
        slider.maxValue= max;
        slider.value= value;
    }
    public void SetSkillIcon(HUDSkillIcon[] list,int keyIndex, ASpell spell) {
        list[keyIndex].Initialize(spell, _mainController);
    }
    void SetKeys() {
        for (int i = 0; i < spellKeys.Length; i++) {
            spellIconList[i].SetKey(spellKeys[i]);
        }
    }
    public void SetSpellDescription(ASpell spell) {
        SpellDescription.GameObject.SetActive(true);

        SpellDescription.GameObject.transform.position = _mainController.Input.GetMouseScreenPos() + new Vector3(0, 100, 0);
        SpellDescription.Name.text = spell.Name;
        SpellDescription.Desc.text = spell.Description;
        SpellDescription.Cooldown.text = spell.CooldownTime.ToString();
         
        SpellDescription.Icon.sprite = spell.Icon;
    }
    public void CloseSpellDescription() {
        SpellDescription.GameObject.SetActive(false);
    }
}