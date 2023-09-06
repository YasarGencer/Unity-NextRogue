using DG.Tweening; 
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuPanel_Play : AUI {
    [SerializeField] AudioClip _deletePlayer, _createPlayer;
    [Header("CAMERA")]
    [SerializeField] Camera _cam;
    [SerializeField] float2 _camScale;
    [Header("Buttons")]
    [SerializeField] Button _backButton;
    [SerializeField] Button _prevButton, _nextButton, _startButton;

    [Header("Panel")]
    [SerializeField] HUDMainmenuPlayer _playerPanel; 

    int _index = 0;
    GameObject _player;
    PlayerList _playerList; 
    UnityAction _loading;
    public void Initialize(PlayerList list, UnityAction back, UnityAction loading)
    {
        _child = transform.GetChild(0);

        _index = 0;
        _cam.orthographicSize = _camScale.x;
        _playerList = list;
        _loading = loading;

        _backButton.onClick.AddListener(back); 
        _prevButton.onClick.AddListener(PrevIndex);
        _nextButton.onClick.AddListener(NextIndex);
        _startButton.onClick.AddListener(StartGame);
        _playerPanel.Initialize();
    }
    public override void Open() {
        base.Open();
            
        float value = _cam.orthographicSize; 
        DOTween.To(() => value, x => value = x, _camScale.y, 1)
        .OnUpdate(() => {
            _cam.orthographicSize = value;
        }).SetEase(Ease.InCirc);

        CreatePlayer();
         
    }
    public override void Close(float time = 0) {
        base.Close(time);
        float value = _cam.orthographicSize;
        DOTween.To(() => value, x => value = x, _camScale.x, 1)
        .OnUpdate(() => {
            _cam.orthographicSize = value;
        }).SetEase(Ease.InCirc);
        _index = 0;
        DestroyPlayer();
    }

    void CreatePlayer() {
        _player = Instantiate(_playerList.GetPlayer(_index).PlayerForUI, Vector2.zero, Quaternion.identity);
        _playerPanel.Open(_playerList.GetPlayer(_index).Stat);
        AudioManager.PlaySound(_createPlayer, null, AudioManager.AudioVolume.ui, false);
    }
    void DestroyPlayer() {
        _player.GetComponent<Animator>().SetTrigger("die");
        AudioManager.PlaySound(_deletePlayer, null, AudioManager.AudioVolume.ui, false);
        Destroy(_player, .5f);
        _playerPanel.Close();
    }
    void PrevIndex() {
        _index = CheckIndex(_index - 1);
        ChangePlayer();
    }
    void NextIndex() {
        _index = CheckIndex(_index + 1);
        ChangePlayer();
    }
    int CheckIndex(int value) {
        if (value > _playerList.GetList().Count - 1)
            return 0;
        else if (value < 0)
            return _playerList.GetList().Count - 1;
        else
            return value;
    }
    void ChangePlayer() {
        DestroyPlayer();
        _prevButton.interactable = false;
        _nextButton.interactable = false;
        Invoke(nameof(CreatePlayer), .5f);
        Invoke(nameof(ButtonInteractables), .5f);
    }
    void ButtonInteractables() {
        _prevButton.interactable = true;
        _nextButton.interactable = true;
    }
    void StartGame()
    {
        GameManager.SetPlayerIndex(_index);
        _loading();
    }
}
