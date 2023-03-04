using UnityEngine;
using static UnityEditor.Progress;

public class MainManager : MonoBehaviour
{
    private static MainManager _instance = null;
    public static MainManager Instance { get { return _instance; } }

    [SerializeField]
    private EventManager _eventManager;
    [SerializeField]
    private GameManager _gameManager;
    [SerializeField]
    private PDGManager _PDGManager;
    [SerializeField]
    private CanvasManager _canvasManager;

    public GameManager GameManager { get { return _gameManager; } }
    public EventManager EventManager { get { return _eventManager; } }
    public PDGManager PDGManager { get { return _PDGManager; } }
    public CanvasManager CanvasManager { get { return _canvasManager; } }

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

        _gameManager.Initialize();
        _eventManager.Initialize();
        
        StartGame();
    }
    public void StartGame() {
        for (int i = 0; i < Enemies.childCount; i++)
            Destroy(Enemies.GetChild(i).gameObject);
        for (int i = 0; i < Enviroment.childCount; i++)
            Destroy(Enviroment.GetChild(i).gameObject);

        _PDGManager.Rooms.ResetValues();
        _PDGManager.Initialize();

        _canvasManager.Initialize();

        _player.GetComponentInChildren<P_MainController>().Initialize(); 

        _eventManager.RunOnGameStart();
    } 
}
