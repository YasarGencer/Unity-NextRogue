using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDSlider : MonoBehaviour { 
    [SerializeField]
    TextMeshProUGUI _showText,_constantText;
    [SerializeField]
    bool _showTextOnlyOnEnter, _showWithMax;
    Slider _slider;
    float _maxValue;
    public void Initialize(float value,float maxValue, float constant) {
        _maxValue = maxValue; 
        TriggerExit();

        _slider = GetComponent<Slider>(); 
        _slider.maxValue = _maxValue;
        SetValue(value);
        SetConstant(constant);
    }
    public void TriggerEnter() {
        _showText.gameObject.SetActive(true);
    }
    public void TriggerExit() {
        if (_showTextOnlyOnEnter)
            _showText.gameObject.SetActive(false);
    }
    public void SetValue(float value) {
        _slider.value = value;
        if(_showWithMax)
            _showText.SetText(value + " / " + _slider.maxValue);
        else
            _showText.SetText(value.ToString());
    }
    public void SetConstant(float value) {
        if(_constantText)
            _constantText.SetText(value.ToString());
    }

    internal void SetValue(float value, float maxValue, int constant) {
        _slider.maxValue = maxValue;
        _slider.value = value;
        if (_showWithMax)
            _showText.SetText(value + " / " + _slider.maxValue);
        else
            _showText.SetText(value.ToString());
        SetConstant(constant);
    }
}
