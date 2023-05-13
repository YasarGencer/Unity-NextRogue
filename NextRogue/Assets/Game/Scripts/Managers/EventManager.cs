using System; 
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public delegate void OnGamePuase();
    public delegate void OnGameUnPuase();
    public delegate void OnGameStart();
    public delegate void OnInteract();
    public delegate void OnPlayerInitialized();

    public event OnGamePuase onGamePause;
    public event OnGameUnPuase onGameUnPause;
    public event OnGameStart onGameStart;
    public event OnInteract onInteract;
    public event OnPlayerInitialized onPlayerInitialized;

    public void Initialize() {
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
}
