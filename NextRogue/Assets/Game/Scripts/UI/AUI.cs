using UnityEngine;
using UnityEngine.UI;

public class AUI : MonoBehaviour
{
    public virtual void Initialize() {
        RegisterEvents();
    }
    public void SetSlider(Slider slider, float max, float value) {
        slider.maxValue = max;
        slider.value = value;
    }
    public virtual void Open() {
        gameObject.SetActive(true);
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
