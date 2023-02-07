using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    private static MainManager _instance = null;
    public static MainManager Instance { get { return _instance; } }

    private P_MainController _mainController;
    public P_MainController MainController { get { return _mainController; } }

    [SerializeField]
    private PDGManager _PDGManager;
    public PDGManager PDGManager { get { return _PDGManager; } }

    [Space(15f)]
    [Header("Scene Managers")]
    [SerializeField]
    private Transform _managers;
    [SerializeField]
    private Transform _utilities;
    [SerializeField]
    private Transform _player;
    [SerializeField]
    private Transform _enviroment;
    [SerializeField]
    private Transform _enemies;
    public Transform Managers { get { return _managers; } }
    public Transform Utilities { get { return _utilities; } }
    public Transform Player { get { return _player; } }
    public Transform Enviroment { get { return _enviroment; } }
    public Transform Enemies { get { return _enemies; } }
    private void Awake() {
        Initialize();
    }
    public void Initialize() {
        _instance = this;

        _mainController = GameObject.FindGameObjectWithTag("Player").GetComponent<P_MainController>() as P_MainController;


        _PDGManager.Initialize();
    }
}
