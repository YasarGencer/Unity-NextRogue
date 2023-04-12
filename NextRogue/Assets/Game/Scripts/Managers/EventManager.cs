using System; 
using UnityEngine;

public class EventManager : MonoBehaviour
{

    public delegate void OnLoad();
    public event OnLoad onLoad;
    //INGAMES
    public delegate void OnGamePuase();
    public delegate void OnGameUnPuase();  
    public delegate void OnInteract();

    public event OnGamePuase onGamePause;
    public event OnGameUnPuase onGameUnPause; 
    public event OnInteract onInteract;

    public void Initialize() {
    }
    public void RunOnLoad() {
        onGamePause = null;
        onGameUnPause = null;
        onInteract = null;
    }
    //INGAMES
    public void RunOnGamePause() { 
        onGamePause();
    }
    public void RunOnGameUnPuase() {
        onGameUnPause();
    } 
    public void RunOnInteract() {
        onInteract();
    }
}
