using UnityEngine;
using UnityEngine.UI;

public class EventManager : MonoBehaviour
{
    public delegate void OnGamePuase();
    public delegate void OnGameUnPuase();
    public delegate void OnGameStart();
    public delegate void OnInteract();
    public delegate void OnPlayerInitialized();
    public delegate void OnTrackStart(AudioClip clip);
    public delegate void OnCoinChange(int value);
    public delegate void OnDOTCycle();

    public event OnGamePuase onGamePause;
    public event OnGameUnPuase onGameUnPause;
    public event OnGameStart onGameStart;
    public event OnInteract onInteract;
    public event OnPlayerInitialized onPlayerInitialized;
    public static event OnTrackStart onTrackStart;
    public event OnCoinChange onCoinChange;
    public event OnDOTCycle onDOTCycle;

    public void Initialize() {
        onTrackStart = null;
    }
    public void RunOnGamePause() { 
        onGamePause();
    }
    public void RunOnGameUnPuase() {
        onGameUnPause();
    }
    public void RunOnGameStart() {
        onGameStart();
    }
    public void RunOnInteract() {
        onInteract();
    } 
    public void PlayerInitialized() {
        onPlayerInitialized();
    }
    public static void RunOnTrackStart(AudioClip clip) {
        if (onTrackStart == null)
            return;
        onTrackStart(clip);
    }
    public void RunOnCoinChange(int value) {
        MainManager.Instance.Player.GetComponentInChildren<P_MainController>().Stats.Coin += value;
        onCoinChange(MainManager.Instance.Player.GetComponentInChildren<P_MainController>().Stats.Coin);
    }
    public void RunOnDOTCycle() {
        if (onDOTCycle == null)
            return;
        onDOTCycle();
    }
}
