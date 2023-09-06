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
    public void NextLevel(bool showLoading)
    {
        _levelSettings.NexlLevel();

        _PDGManager?.Rooms.ResetValues();
        _PDGManager?.TilemapVisualizer.Clear();

        if (activeMap)
            Destroy(activeMap);

        ActiveLevelSetting = _levelSettings.GetLevelSettings();

        if (showLoading)
        {
            LoaderElement _loader = ActiveLevelSetting.Loader;
            MainManager.Instance.CanvasManager.Loading.Open(_loader, NextLevelSegment);
        }
        else
        {
            NextLevelSegment();
        }
    }
    void NextLevelSegment()
    {
        if (ActiveLevelSetting.DungeonLevel.IsRandomDungeon)
        {
            _PDGManager?.Initialize();
        }
        else if (ActiveLevelSetting.ShopLevel.IsShop)
        {
            activeMap = Instantiate(ActiveLevelSetting.ShopLevel.ShopMap);
        }
        else if (ActiveLevelSetting.TutorialLevel.IsTutorial)
        {
            activeMap = Instantiate(ActiveLevelSetting.TutorialLevel.TutorialMap);
        }

        MainManager.Instance.CanvasManager.Loading.Close();
    }
}