using UnityEngine;

public class GameManager : MonoBehaviour {
    bool _gamePaused;
    public bool GamePaused { get { return _gamePaused; } set { _gamePaused = value; } }
    
    [SerializeField]
    private ChallangeManager _challangeManager;
    [SerializeField]
    private DOTManager _dotManager;

    [SerializeField] AllSpells _spellList;
    [SerializeField] PlayerList _playerList;
    [SerializeField] AudioClip _openingClip;
    public AllSpells AllSpells { get { return _spellList; }}
    public PlayerList PlayerList { get { return _playerList; }}
    public ChallangeManager ChallangeManager { get { return _challangeManager; } }
    public DOTManager DOTManager { get { return _dotManager; } }

    public void Initialize() { 
        MainManager.Instance.EventManager.onGamePause += OnGamePause;
        MainManager.Instance.EventManager.onGameUnPause += OnGameUnPause;

        _spellList.Initialize();

        foreach (var item in _playerList.GetList())
            item.Stat.Initialize();

        _challangeManager?.Initialize();
        _dotManager?.Initialize();

        if (MainManager.Instance.IsTest)
            return;


        Instantiate(_playerList.GetPlayer(GetPlayerIndex() % _playerList.GetCount()).Player, MainManager.Instance.Player);

        if (_playerList.GetPlayer(GetPlayerIndex() % _playerList.GetCount()).Stat.GetTutorial() == false)
            LevelSettings.SetIfTutorial(GetPlayerIndex() % _playerList.GetCount());

    }
    void OnGamePause() {
        _gamePaused = true;
    }
    void OnGameUnPause() {
        _gamePaused = false;
    }
    public static int GetPlayerIndex() {
        return PlayerPrefs.GetInt("Player", 0);
    }
    public static void SetPlayerIndex(int value) {
        PlayerPrefs.SetInt("Player", value);
    }
    public void PlayOpening() {
        AudioManager.PlaySound(_openingClip, null, AudioManager.AudioVolume.environment, false);
    }
}
