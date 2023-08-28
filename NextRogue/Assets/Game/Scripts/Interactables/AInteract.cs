using DG.Tweening;
using TMPro;
using UnityEngine;

public abstract class AInteract : MonoBehaviour {

    protected bool _isInteractable; 
    [SerializeField]
    protected GameObject _info;
    protected Animator _animator;
    private Tweener currentTween;

    private void Start() => OnStart();
    protected virtual void OnStart() {
        MainManager.Instance.EventManager.onInteract += CheckInteraction;
        _animator = GetComponent<Animator>();
    }
    protected void CheckInteraction() {
        if (!MainManager.Instance.GameManager.GamePaused)
            if (_isInteractable)
                Interact();
    }
    protected virtual void Interact() {
        Info(false);
    } 
    private void OnDrawGizmos() { 
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.CompareTag("Player"))
            Info(true);
    }
    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Player"))
            Info(false);
    }
    protected virtual void Info(bool value)
    {
        _isInteractable = value;
        if (value)
        {
            _info.SetActive(true);
        }
        else
        {
            TextMeshProUGUI text = _info.GetComponent<TextMeshProUGUI>();

            if (text != null)
            {
                text.alpha = 1.0f;
                currentTween = text.DOFade(0.0f, 0.5f)
                    .SetEase(Ease.OutQuad).
                    OnComplete(() =>
                    {
                        _info.SetActive(false);
                    });

            }
            else
            {
                _info.SetActive(false);
            }
        }
    }
    protected void InfoText(string str) {
        TextMeshProUGUI text = _info.GetComponent<TextMeshProUGUI>();
        if(currentTween != null)
            currentTween.Kill();
        text.SetText(str);
        text.alpha = 0.0f;
        text.DOFade(1.0f, 0.5f)
            .SetEase(Ease.InQuad);
    }
}
