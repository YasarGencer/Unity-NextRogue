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
    public void Initialize(P_MainController mainController, float maxHealth) {
        _mainController = mainController;
        _show.SetActive(false);
        _slider = GetComponent<Slider>();

        _slider.maxValue = maxHealth;
        _slider.value = _slider.maxValue;
    }
    public void TriggerEnter() {
        _show.SetActive(true);
    }
    public void TriggerExit() {
        _show.SetActive(false);
    }
    public void SetHealth(float value) {
        _slider.value = value;
        _text.SetText(value.ToString());
        //_text.SetText(health + "/" + maxHealth);
    }
}
