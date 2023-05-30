using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class AUI : MonoBehaviour
{ 
    protected Transform _child;
    public bool isOpen { get; protected set; }
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
        isOpen = true;

        _child.gameObject.SetActive(true);
        _child.localPosition = new(0, -50, 0);
        _child.DOLocalMove(Vector3.zero, 1f).SetEase(Ease.InCirc); 

        GetComponent<CanvasGroup>().alpha = 0;
        GetComponent<CanvasGroup>().DOFade(1,1);  
    }
    public virtual void Close(float time = 0) {
        isOpen = false;
        if (time == 0) {
            gameObject.SetActive(false);
            _child.localPosition = new(0, 0, 0);
            return;
        }
        _child.localPosition = new(0, -50, 0);
        _child.DOLocalMove(Vector3.zero, 1f).SetEase(Ease.InCirc);
         
        GetComponent<CanvasGroup>().DOFade(0, time).OnComplete(()=> gameObject.SetActive(false));
    }
    public virtual void ButtonPressed(Image buttonSprite = null) {
        if (buttonSprite) {
            var color = buttonSprite.color;
            color.a = isOpen == true ? 0.25f : 1;
            buttonSprite.color = color;
        }
        if (isOpen)
            Close(); 
        else
            Open();  
    }
    // EVENTS 
    protected virtual void RegisterEvents() {
        MainManager.Instance.EventManager.onGamePause += OnGamePause;
        MainManager.Instance.EventManager.onGameUnPause += OnGameUnPause;
        MainManager.Instance.EventManager.onGameStart += OnGameStart;
        MainManager.Instance.EventManager.onPlayerInitialized += OnPlayerInitialized;
    }
    protected virtual void OnGamePause() {
    }
    protected virtual void OnGameUnPause() {
    }
    protected virtual void OnGameStart() { 
    }
    protected virtual void OnPlayerInitialized() { 
    }
}
