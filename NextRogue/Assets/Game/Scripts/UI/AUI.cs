using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class AUI : MonoBehaviour
{ 
    protected Transform _child;
    public virtual void Initialize() {
        RegisterEvents();
        _child = transform.GetChild(0); 
        _child.gameObject.SetActive(false);
    }
    public void SetSlider(Slider slider, float max, float value) {
        slider.maxValue = max;
        slider.value = value;
    }
    public virtual void Open() {
        gameObject.SetActive(true);

        _child.gameObject.SetActive(true);
        _child.localPosition = new(0, -50, 0);
        _child.DOLocalMove(Vector3.zero, 1f).SetEase(Ease.InCirc);

        var alpha = GetComponent<CanvasGroup>();
        float alphaValue = 0; 

        alpha.alpha = 0;

        DOTween.To(() => alphaValue, x => alphaValue = x, 1, 1)
        .OnUpdate(() => {
            alpha.alpha = alphaValue;
        }).SetEase(Ease.InCirc);

    }
    public virtual void Close() {
        gameObject.SetActive(false);
    }

    // EVENTS 
    protected virtual void RegisterEvents() {
        MainManager.Instance.EventManager.onGamePause += OnGamePause;
        MainManager.Instance.EventManager.onGameUnPause += OnGameUnPause;
        MainManager.Instance.EventManager.onGameStart += OnGameStart;
    }
    protected virtual void OnGamePause() {
    }
    protected virtual void OnGameUnPause() {
    }
    protected virtual void OnGameStart() {

    }
}
