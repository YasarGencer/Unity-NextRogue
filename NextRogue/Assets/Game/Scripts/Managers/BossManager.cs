
using System.Collections.Generic;
using UnityEngine; 
using UnityEngine.InputSystem;

public class BossManager : MonoBehaviour { 
    P_MainController player { get { return MainManager.Instance.Player.GetComponentInChildren<P_MainController>(); } }

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
    }
    private void Awake() {
        MainManager.Instance.EventManager.onInteract += ComicSceneNextStep;
        NextPhase();
    }
    void Initialize() {
        MainManager.Instance.OpeningAnim();
        onComicScene = false;
        player.Initialize();
        CloseAllMaps();
        activePhase.Map.SetActive(true);
        activePhase.ComicCanvas.SetActive(false);
        activePhase.Boss.Initialize();
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
}