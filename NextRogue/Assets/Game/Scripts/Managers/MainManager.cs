using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    private static MainManager _instance;
    public static MainManager Instance { get { return _instance; } }

    private P_MainController _mainController;
    public P_MainController MainController { get { return _mainController; } }

    [SerializeField]
    private PDGManager _PDGManager;

    public PDGManager PDGManager { get { return _PDGManager; } }
    private void Awake() {
        Initialize();
    }
    public void Initialize() {
        _instance = this;
        _mainController = GameObject.FindGameObjectWithTag("Player").GetComponent<P_MainController>() as P_MainController;
        _PDGManager.Initialize();
    }
}
