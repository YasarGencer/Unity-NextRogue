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

        GetComponent<CanvasGroup>().DOFade(0, 0);
        GetComponent<CanvasGroup>().DOFade(1, 1).SetEase(Ease.InCirc);

    }
    public virtual void Close() {
        gameObject.SetActive(false);
    }

    // EVENTS 
    protected virtual void RegisterEvents() {
        MainManager.Instance.EventManager.onGamePause += OnGamePause;
        MainManager.Instance.EventManager.onGameUnPause += OnGameUnPause;
    }
    protected virtual void OnGamePause() {
    }
    protected virtual void OnGameUnPause() {
    }
}
