
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
    Phase activePhase { get { return phases[activePhaseCount]; } }

    [System.Serializable]
    struct Phase {
        public GameObject Map;
        public B_MainController Boss;
        public Animator Animator;
        public GameObject ComicCanvas;
        public int ComicFrameCount;
        public GameObject CinemachineCamera;
    }
    private void Awake() {
        MainManager.Instance.EventManager.onInteract += ComicSceneNextStep;
        NextPhase();
    }
    async void Initialize() {
        MainManager.Instance.OpeningAnim();
        onComicScene = false;
        player.Initialize();
        CloseAllMaps();
        activePhase.Map.SetActive(true);
        activePhase.ComicCanvas.SetActive(false);
        bossSliderObject.DOFade(0, 0);
        await Task.Delay(1000);
        activePhase.CinemachineCamera.SetActive(false);
        activePhase.Boss.Initialize();
        bossSliderObject.DOFade(1, 1);
        bossSlider.maxValue = 1;
        bossSlider.value = 0;
        bossSlider.DOValue(1, 2f);
    }
    public void NextPhase() {
        activePhaseCount++;
        currentFrame = 0;
        onComicScene = true;
    }
    public void ComicSceneNextStep() {
        if(onComicScene == false)
            return;
        if(activePhase.ComicFrameCount > currentFrame) {
            activePhase.Animator.SetTrigger("Next");
            currentFrame++;
        } else {
            ComicSceneEnd();
        }
    }
    public void ComicSceneEnd() {
        Initialize();
    }
    private void Update() {
        if(Keyboard.current.eKey.wasPressedThisFrame)
            ComicSceneNextStep();
    }
    void CloseAllMaps() {
        foreach(var item in phases) {
            item.Map.SetActive(false);
        }
    }
    public void UpdateHealthBar() {
        bossSlider.maxValue = activePhase.Boss.Stats.MaxHealth;
        bossSlider.value = activePhase.Boss.Stats.Health;
    }
}