using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDHealthBar : MonoBehaviour
{
    P_MainController _mainController;
    [SerializeField]
    GameObject _show;
    [SerializeField]
    TextMeshProUGUI _text;
    Slider _slider;
    public void Initialize(P_MainController mainController) {
        _mainController = mainController;
        _show.SetActive(false);
        _slider = GetComponent<Slider>();
        _slider.value = _slider.maxValue;
    }
    public void TriggerEnter() {
        _show.SetActive(true);
        SetHealth();
    }
    public void TriggerExit() {
        _show.SetActive(false);
    }
    public void SetHealth() {
        var health = _mainController.Stats.Health;
        var maxHealth = _mainController.Stats.MaxHealth;
        _slider.maxValue = maxHealth;
        _slider.value = health;
        _text.SetText(health.ToString());
        //_text.SetText(health + "/" + maxHealth);
    }
}
