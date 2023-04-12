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

    public GameManager GameManager { get { return _gameManager; } }
    public EventManager EventManager { get { return _eventManager; } } 
     
    private void Awake() {
        Initialize();
    }
    public void Initialize() {
        _instance = this;

        _eventManager.Initialize();
        _gameManager.Initialize();
    }
}
