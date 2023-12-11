using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("DEBUG")]
    public int chooseLevel = -1;
    public int chooseTutorial = -1;
    [Space(20f)]
    [SerializeField]
    private LevelSettings _levelSettings;
    [SerializeField]
    private PDGManager _PDGManager;
    public PDGManager PDGManager { get { return _PDGManager; } }

    private GameObject activeMap;

    public LevelSetting ActiveLevelSetting { get; private set; }
    public void Initialize()
    {
        _levelSettings.SetLevel(-1);
    }
    public int GetLevel()
    {
        return _levelSettings.GetLevel();
    }
    public void SetLevel(int level)
    {
        _levelSettings.SetLevel(level);
    }
    public void NextLevel()
    {
        _levelSettings.NexlLevel();

        _PDGManager?.Rooms.ResetValues();
        _PDGManager?.TilemapVisualizer.Clear();

        if (activeMap)
            Destroy(activeMap);

        ActiveLevelSetting = _levelSettings.GetLevelSettings();
        NextLevelSegment();
    }
    void NextLevelSegment()
    {
        MainManager.Instance.SecondPhase();
        if (ActiveLevelSetting.MyType == DisplayOption.Dungeon) {
            _PDGManager?.Initialize();
        }else 
            activeMap = Instantiate(ActiveLevelSetting.NonDungeonLevel.Map);
    }
}