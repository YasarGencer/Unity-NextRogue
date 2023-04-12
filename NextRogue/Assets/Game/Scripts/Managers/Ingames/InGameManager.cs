using Cinemachine;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using static EventManager;

public class InGameManager : MonoBehaviour { 
    public static InGameManager Instance { get; private set; }

    [SerializeField] Transform _utilities, _managers, _enviroment, _player, _enemies;
    public Transform Utilities { get { return _utilities; } }
    public Transform Managers { get { return _managers; } }
    public Transform Enviroment{ get { return _enviroment; } } 
    public Transform Enemies { get { return _enemies; } }

    [SerializeField] PDGManager _pdgManager;
    [SerializeField] CanvasManager _canvasManager;
    public PDGManager PDGManager { get { return _pdgManager; } }
    public CanvasManager CanvasManager { get { return _canvasManager; } }

    bool _gamePaused;
    public bool GamePaused { get { return _gamePaused; } set { _gamePaused = value; } }

    private void Start() {
        SceneManager.SetActiveScene(SceneManager.GetSceneAt(1));
        Initialize();
    }
    public void Initialize() {
        Instance = this;

        Instantiate(MainManager.Instance.GameManager.PlayerList.GetActivePlayer().Player, _player).GetComponent<P_MainController>().Initialize();

        MainManager.Instance.EventManager.onGamePause += OnGamePause;
        MainManager.Instance.EventManager.onGameUnPause += OnGameUnPause;

        var cinemachine = GameObject.FindObjectOfType<CinemachineVirtualCamera>();
        cinemachine.Follow = _player.GetChild(0);
        var defSize = cinemachine.m_Lens.OrthographicSize;
        float size = 2;
        DOTween.To(() => size, x => size = x, defSize, 2)
        .OnUpdate(() => {
            cinemachine.m_Lens.OrthographicSize = size;
        }).SetEase(Ease.InCirc);

        _pdgManager.Initialize();
        _canvasManager.Initialize();
    } 
    public void Restart() {
        MainManager.Instance.GameManager.LoadScene(2,2);
    }

    void OnGamePause() {
        _gamePaused = true;
    }
    void OnGameUnPause() {
        _gamePaused = false;
    }
}
