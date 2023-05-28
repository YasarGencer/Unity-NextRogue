using DG.Tweening;
using TMPro;
using UnityEditor;
using UnityEngine; 
using UnityEngine.UI; 

public class HUDMainmenuButton : MonoBehaviour
{
    [SerializeField] RectTransform _Line;
    TextMeshProUGUI _text;
    [SerializeField] Button _Button;
    float _lineX, _textSize;
    [SerializeField] AudioClip _clickSound;
    private void Awake() {
        _text = GetComponent<TextMeshProUGUI>();
        _textSize = _text.fontSize;
        _lineX = _Line.sizeDelta.x;
        _Line.sizeDelta = new Vector2(0, _Line.sizeDelta.y);
        _Button.onClick.AddListener(ClickSound); 
    }
    public void TriggerEnter() { 
        var value = _Line.sizeDelta.x;
        DOTween.To(() => value, x => value = x, _lineX, .3f)
        .OnUpdate(() => {
            _Line.sizeDelta = new Vector2(value, _Line.sizeDelta.y);
        });
        var value2 = _text.fontSize;
        DOTween.To(() => value2, x => value2 = x, _textSize * 1.5f, .3f)
        .OnUpdate(() => {
            _text.fontSize = value2;
        });
    }
    public void TriggerExit() {
        var value = _Line.sizeDelta.x;
        DOTween.To(() => value, x => value = x, 0, .3f)
        .OnUpdate(() => {
            _Line.sizeDelta = new Vector2(value, _Line.sizeDelta.y);
        });
        var value2 = _text.fontSize;
        DOTween.To(() => value2, x => value2 = x, _textSize, .3f)
        .OnUpdate(() => {
            _text.fontSize = value2;
        });
    }
    void ClickSound() {
        AudioManager.PlaySound(_clickSound);
    }
}
