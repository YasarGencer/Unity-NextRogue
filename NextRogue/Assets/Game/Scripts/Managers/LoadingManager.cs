using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingManager : AUI
{
    [Header("Loader")]
    [SerializeField] Image background;
    [SerializeField] TextMeshProUGUI messageBox;
    [SerializeField] Vector2 messageRange = new(2, 6);
    string[] messageList;
    float currentMessage;
    float messageCount;

    [Space, Header("Bar")]
    [SerializeField] Image fillBar;
    [SerializeField] TextMeshProUGUI percentText;
    [SerializeField] Vector2 speedRange = new(0.5f, 3.0f);
    [SerializeField] Vector2 timeRange = new(0.05f, 0.1f);


    [SerializeField] bool skipLoading = false;
    LoaderElement loader;
    UnityAction quest;

    public override void Initialize()
    {
        _child = transform.GetChild(0);
    }
    void StartLoading()
    {
        if(loader == null)
        {
            if (quest != null)
            {
                quest();
            }
            else
            {
                Close();
            }
            return;
        }
        float currentFillSpeed = Random.Range(speedRange.x, speedRange.y);
        float fillAmount = currentFillSpeed * Time.deltaTime;
        float newFillAmount = Mathf.Clamp01(fillBar.fillAmount + fillAmount);

        fillBar.DOFillAmount(Mathf.Clamp01(newFillAmount), Random.Range(timeRange.x, timeRange.y))
            .SetEase(Ease.Linear).OnUpdate(() =>
            {
                percentText.text = $"{newFillAmount * 100:F0}%";
            })
            .OnComplete(() =>
            {
                if (fillBar.fillAmount < 1)
                {
                    if (fillBar.fillAmount > (currentMessage + 1) * (1 / messageCount))
                    {
                        currentMessage++;
                        messageBox.text = messageList[(int)currentMessage];
                    }
                    StartLoading();
                }
                else
                {
                    if (quest != null)
                    {
                        quest();
                    }
                    else
                    {
                        Close();
                    }
                }
            });
    }
    void SetupUIElements()
    {
        background.sprite = loader.loadingSprite;
        percentText.text = "%0";
        percentText.color = loader.textColor;
        fillBar.fillAmount = 0;
        if(skipLoading) { fillBar.fillAmount = 99; }
        fillBar.color = loader.barColor;
        messageBox.text = messageList[(int)currentMessage];
        transform.GetChild(0).gameObject.SetActive(true);
        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
    }
    void SetupRandomMessages()
    {
        currentMessage = 0;
        messageCount = Mathf.Clamp(Random.Range((int)messageRange.x, (int)messageRange.y + 1), 0, loader.GetTextCount());
        messageList = new string[(int)messageCount];

        HashSet<string> usedMessages = new();

        for (int i = 0; i < messageCount; i++)
        {
            string randomMessage;

            do
            {
                randomMessage = loader.loadingTexts[Random.Range(0, loader.GetTextCount())];
            }
            while (usedMessages.Contains(randomMessage));

            usedMessages.Add(randomMessage);

            messageList[i] = randomMessage;
        }
    }
    public void Open(LoaderElement _player, UnityAction _quest)
    {
        quest = _quest;
        loader = _player;
        if (loader != null)
        {
            SetupRandomMessages();
            SetupUIElements();
        }
        gameObject.SetActive(true);
        isOpen = true;

        if (_openClip)
            AudioManager.PlaySound(_openClip, null, AudioManager.AudioVolume.ui, false);

        _child.gameObject.SetActive(true);

        GetComponent<CanvasGroup>().alpha = 0;
        GetComponent<CanvasGroup>().DOFade(1, 1);

        Invoke(nameof(StartLoading), 1f);
    }
}
