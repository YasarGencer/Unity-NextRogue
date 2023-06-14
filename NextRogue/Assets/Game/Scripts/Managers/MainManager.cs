using Cinemachine;
using DG.Tweening; 
using UnityEngine; 

public class MainManager : MonoBehaviour
{
    public bool IsTest;
    private static MainManager _instance = null;
    public static MainManager Instance { get { return _instance; } }

    [SerializeField]
    private TestManager _testManager;
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
    [SerializeField]
    private InputManager _inputManager;

    public TestManager TestManager { get { return _testManager; } }
    public GameManager GameManager { get { return _gameManager; } }
    public EventManager EventManager { get { return _eventManager; } }
    public LevelManager LevelManager { get { return _levelManager; } }
    public CanvasManager CanvasManager { get { return _canvasManager; } } 
    public InputManager InputManager { get { return _inputManager; } } 

    [Space(15f)]
    [Header("Scene Managers")]
    [SerializeField]
    private Transform _managers;
    [SerializeField]
    private Transform _utilities;
    [SerializeField]
    private Transform _player;
    [SerializeField]
    private Transform _environment;
    [SerializeField]
    private Transform _enemies;
    public Transform Managers { get { return _managers; } }
    public Transform Utilities { get { return _utilities; } }
    public Transform Player { get { return _player; } }
    public Transform Environment { get { return _environment; } }
    public Transform Enemies { get { return _enemies; } }
    private void Awake() {
        Initialize();
        OpeningAnim();
    }
    public void Initialize() { 
        AudioManager.CreateAudioManager();

        _instance = this;

        _gameManager?.Initialize(); 
        _eventManager?.Initialize(); 
        _levelManager?.Initialize();
        StartGame();
    }
    public void StartGame() { 
        if(Enemies!= null)
            for (int i = 0; i < Enemies.childCount; i++)
                Destroy(Enemies.GetChild(i).gameObject);
        if (Environment != null)
            for (int i = 0; i < Environment.childCount; i++)
                Destroy(Environment.GetChild(i).gameObject);

        _canvasManager?.Initialize();
        _player?.GetComponentInChildren<P_MainController>().Initialize();
         
        _levelManager?.NextLevel();  

        _eventManager?.RunOnGameStart();

        _testManager?.Initialize();
    } 
    public void PlayerInitialized() {
        MainManager.Instance.EventManager.PlayerInitialized();
        GameObject.FindObjectOfType<TutorialManager>()?.OnPlayerInitialized();
        _inputManager.Initialize();
        GameObject.FindObjectOfType<CameraTarget>().Initialize();
    }
    void OpeningAnim() {
        _gameManager.PlayOpening();
        var cinemachine = Utilities.GetChild(1).GetComponent<CinemachineVirtualCamera>();
        var defSize = cinemachine.m_Lens.OrthographicSize;
        float size = 2;
        DOTween.To(() => size, x => size = x, defSize, 2)
        .OnUpdate(() => {
            cinemachine.m_Lens.OrthographicSize = size;
        }).SetEase(Ease.InCirc);
    }
}
