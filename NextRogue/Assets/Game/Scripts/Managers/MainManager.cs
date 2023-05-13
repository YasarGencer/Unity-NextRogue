using Cinemachine;
using DG.Tweening;
using UnityEngine; 

public class MainManager : MonoBehaviour
{
    private static MainManager _instance = null;
    public static MainManager Instance { get { return _instance; } }

    [SerializeField]
    private EventManager _eventManager;
    [SerializeField]
    private GameManager _gameManager;
    [SerializeField]
    private LevelManager _levelManager;
    [SerializeField]
    private PDGManager _PDGManager;
    [SerializeField]
    private CanvasManager _canvasManager;

    public GameManager GameManager { get { return _gameManager; } }
    public EventManager EventManager { get { return _eventManager; } }
    public LevelManager LevelManager { get { return _levelManager; } }
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
        var cinemachine = Utilities.GetChild(1).GetComponent<CinemachineVirtualCamera>();
        cinemachine.Follow = Player.GetChild(0);
        var defSize = cinemachine.m_Lens.OrthographicSize;
        float size = 2;
        DOTween.To(() => size, x => size = x, defSize, 2)
        .OnUpdate(() => {
            cinemachine.m_Lens.OrthographicSize = size;
        }).SetEase(Ease.InCirc);
    }
    public void Initialize() {
        _instance = this;

        _gameManager.Initialize();
        _eventManager.Initialize();
        _levelManager.Initialize();

        StartGame();
    }
    public void StartGame() {
        for (int i = 0; i < Enemies.childCount; i++)
            Destroy(Enemies.GetChild(i).gameObject);
        for (int i = 0; i < Enviroment.childCount; i++)
            Destroy(Enviroment.GetChild(i).gameObject);

        _canvasManager?.Initialize();
        _player?.GetComponentInChildren<P_MainController>().Initialize();

        _levelManager.NextLevel();  

        _eventManager?.RunOnGameStart();

    } 
}
