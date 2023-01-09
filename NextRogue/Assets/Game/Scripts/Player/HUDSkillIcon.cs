using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class HUDSkillIcon : MonoBehaviour {

    public Slider Slider;
    public TextMeshProUGUI Name;
    public TextMeshProUGUI Key;
    public Image[] Icon;
    public void SetIcon(ASpell spell) {
        foreach (var item in Icon) {
            item.sprite = spell.Icon;
        }
        Name.text = spell.Name;
    }
    public void SetKey(string key) {
        Key.text = key;
    }
}
