
using DG.Tweening;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class BossManager : MonoBehaviour { 
    P_MainController player { get { return MainManager.Instance.Player.GetComponentInChildren<P_MainController>(); } }

    [SerializeField] CanvasGroup bossSliderObject;
    [SerializeField] Slider bossSlider;
    [SerializeField] List<Phase> phases;
    int activePhaseCount = -1;
    int currentFrame = -1;
    bool onComicScene = false;
    public Phase ActivePhase { get { return phases[activePhaseCount]; } }

    [System.Serializable]
    public struct Phase {
        public GameObject Map;
        public B_MainController Boss;
        public Animator Animator;
        public GameObject ComicCanvas;
        public int ComicFrameCount;
        public GameObject CinemachineCamera;
    }
    private void Awake() {
#if UNITY_EDITOR
        MainManager.Instance.EventManager.onComicUpdate += ComicSceneNextStep;
#endif
        foreach (var item in phases) {
            item.Map.SetActive(false);
        }
        NextPhase();
    }
    async void Initialize() {
        MainManager.Instance.OpeningAnim();
        onComicScene = false;
        player.Initialize();
        CloseAllMaps();
        ActivePhase.Map.SetActive(true);
        ActivePhase.ComicCanvas.SetActive(false);
        bossSliderObject.DOFade(0, 0);
        await Task.Delay(1000);
        ActivePhase.CinemachineCamera.SetActive(false);
        ActivePhase.Boss.Initialize();
        bossSliderObject.DOFade(1, 1);
        bossSlider.maxValue = 1;
        bossSlider.value = 0;
        bossSlider.DOValue(1, 2f);
    }
    public static void StaticNextPhase() {
        GameObject.FindObjectOfType<BossManager>().NextPhase();
    }
    public void NextPhase() { 
        activePhaseCount++;
        currentFrame = 0;
        onComicScene = true;
        ActivePhase.Animator.gameObject.SetActive(true);
        ActivePhase.ComicCanvas.SetActive(true);
    }
    public void ComicSceneNextStep() {
        if(onComicScene == false)
            return;
        if(ActivePhase.ComicFrameCount > currentFrame) {
            ActivePhase.Animator.SetTrigger("Next");
            currentFrame++;
        } else {
            ComicSceneEnd();
        }
    }
    public void ComicSceneEnd() {
        Initialize();
    }
#if UNITY_EDITOR
    private void Update() {
        if ((Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame) || (Gamepad.current != null && Gamepad.current.dpad.up.wasPressedThisFrame))
            ComicSceneNextStep();
    }
#endif
    void CloseAllMaps() {
        foreach(var item in phases) {
            item.Animator.gameObject.SetActive(false);
        }
    }
    public void UpdateHealthBar() {
        if (ActivePhase.Boss.Stats == null)
            return;
        bossSlider.maxValue = ActivePhase.Boss.Stats.MaxHealth;
        bossSlider.value = ActivePhase.Boss.Stats.Health;
    }
}