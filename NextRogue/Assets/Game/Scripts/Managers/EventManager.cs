using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public delegate void OnGamePuase();
    public event OnGamePuase onGamePause;
    public delegate void OnGameUnPuase();
    public event OnGameUnPuase onGameUnPause;

    public void Initialize() {
    }
    public void RunOnGamePause() { 
        onGamePause();
    }
    public void RunOnGameUnPuase() {
        onGameUnPause();
    }
}
