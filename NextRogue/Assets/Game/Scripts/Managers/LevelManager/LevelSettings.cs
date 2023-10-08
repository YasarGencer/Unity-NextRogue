using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelSettings", menuName = "ScriptableObjects/LevelSettings", order = 0)]
public class LevelSettings : ScriptableObject
{
    [SerializeField] List<LevelSetting> levels;
    public List<LevelSetting> Levels => levels;

    [SerializeField] List<LevelSetting> tutorialLevels;
    public List<LevelSetting> TutorialLevels => tutorialLevels;

    [SerializeField] int activeLevelIndex;
    public LevelSetting GetLevelSettings()
    {
        if (MainManager.Instance.LevelManager.chooseTutorial != -1)
            return tutorialLevels[MainManager.Instance.LevelManager.chooseTutorial];
        else if (MainManager.Instance.LevelManager.chooseLevel != -1)
            return levels[MainManager.Instance.LevelManager.chooseLevel];
        else if (GetIfTutorial())
            return tutorialLevels[GetTutorialLevel()];
        else
            return levels[GetLevel()];
    }
    public int GetLevel()
    {
        return activeLevelIndex % levels.Count;
    }
    public static int GetActiveLevel()
    {
        return PlayerPrefs.GetInt("activeLevel", -1);
    }
    public static int GetTutorialLevel()
    {
        return PlayerPrefs.GetInt("tutorialLevel", 0);
    }
    public static bool GetIfTutorial()
    {
        return PlayerPrefs.GetInt("isTutorial", 0) != 0;
    }
    public static void SetIfTutorial(int index = -1)
    {
        if (index == -1)
            PlayerPrefs.SetInt("isTutorial", 0);
        else
            PlayerPrefs.SetInt("isTutorial", 1);

        PlayerPrefs.SetInt("tutorialLevel", index);
    }
    public void SetLevel(int value)
    {
        PlayerPrefs.SetInt("activeLevel", value);
        activeLevelIndex = GetActiveLevel();
    }
    public void NexlLevel()
    {
        if (GetIfTutorial() == true)
            return;
        PlayerPrefs.SetInt("activeLevel", GetActiveLevel() + 1);
        activeLevelIndex = GetActiveLevel();
    }
}

[System.Serializable]
public class LevelSetting
{
    public string Name;
    [TextArea]
    public string Description;
    public LoaderElement Loader;

    public DisplayOption MyType;

    public DungeonLevel DungeonLevel;
    public ShopLevel ShopLevel;
    public TutorialLevel TutorialLevel;
    public BossLevel BossLevel;

    public bool showme = true;
}

[System.Serializable]
public struct DungeonLevel 
{
    public SimpleRandomWalkData SRWData;

    public int CorridorLength;
    public int CorridorCount;
    [Range(0.1f, 1f)] public float RoomPercent;

    public PDG_RoomProps RoomProps;

    public TilemapVisualizerData TilemapVisualizerData;
}

[System.Serializable]
public struct ShopLevel
{
    public GameObject ShopMap;
}

[System.Serializable]
public struct TutorialLevel
{
    public GameObject TutorialMap;
}
[System.Serializable]
public struct BossLevel {
    public GameObject BossMap;
}

public enum DisplayOption
{
    Dungeon,
    Shop,
    Tutorial,
    Boss
}