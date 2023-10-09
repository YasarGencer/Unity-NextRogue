using DG.Tweening;
using UnityEngine;

public class ShopItemShake : MonoBehaviour
{
    private Tweener currentTween;
    private bool onMove = false;

    Vector3 originalPosition;
    Vector3[] Path = new Vector3[4];

    [SerializeField] float moveDuration = 1f;
    [SerializeField] float stopDuration = 0.25f;
    [SerializeField] float moveDistance = 0.23f;

    private void Start()
    {
        originalPosition = transform.position;
        SetPath();
    }

    void SetPath()
    {
        Path[0] = originalPosition + new Vector3(0, moveDistance, 0);
        Path[1] = originalPosition;
        Path[2] = originalPosition + new Vector3(0, -moveDistance, 0);
        Path[3] = originalPosition;
    }
    public void Shake(bool value) {
        switch (value) {
            case true:
                if (onMove) { return; }

                onMove = true;
                currentTween = transform.DOPath(Path, moveDuration)
                    .SetEase(Ease.Linear)
                    .SetLoops(-1)
                    .OnKill(() => { onMove = false; currentTween = null; });
                break;
            case false:
                if (currentTween != null) {
                    if (currentTween.IsActive()) {
                        currentTween.Kill();

                    }
                }
                transform.DOMoveY(originalPosition.y, stopDuration)
                    .SetEase(Ease.Linear);
                break; 
        }
    }
    private void OnDestroy() {
        currentTween.Kill();
    }
}
