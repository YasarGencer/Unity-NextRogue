using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;

public class MainGUIHUD : MonoBehaviour
{
    PlayerMainController _mainController;
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
        public TextMeshProUGUI Cost;
        public Image Icon;
    }
    public void Initialize(PlayerMainController mainController) {
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
        Debug.Log("b");
        SpellDescription.GameObject.SetActive(true);

        SpellDescription.GameObject.transform.position = _mainController.Input.GetMouseScreenPos() + new Vector3(0, 100, 0);
        SpellDescription.Name.text = spell.Name;
        SpellDescription.Desc.text = spell.Description;
        SpellDescription.Cooldown.text = spell.CooldownTime.ToString();
        SpellDescription.Cost.text = "unavaiable";

        SpellDescription.Icon.sprite = spell.Icon;
    }
    public void CloseSpellDescription() {
        SpellDescription.GameObject.SetActive(false);
    }
}