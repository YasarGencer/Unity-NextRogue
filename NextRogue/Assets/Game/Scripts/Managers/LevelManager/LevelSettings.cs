using UnityEngine; 

[CreateAssetMenu(fileName = "LevelSettings", menuName = "ScriptableObjects/LevelSettings", order = 0)]
public class LevelSettings : ScriptableObject {
    [SerializeField] LevelSetting[] levels;
    [SerializeField] LevelSetting[] tutorialLevels;
    [SerializeField] int activeLevelIndex;
    public LevelSetting GetLevelSettings() {
        if (MainManager.Instance.LevelManager.chooseTutorial != -1)
            return tutorialLevels[MainManager.Instance.LevelManager.chooseTutorial]; 
        else if(MainManager.Instance.LevelManager.chooseLevel != -1)
            return levels[MainManager.Instance.LevelManager.chooseLevel];
        else if(GetIfTutorial())
            return tutorialLevels[GetTutorialLevel()];
        else
            return levels[GetLevel()];
    }
    public int GetLevel() {
        return activeLevelIndex % levels.Length;
    }
    public static int GetActiveLevel() { 
        return PlayerPrefs.GetInt("activeLevel", -1);
    }
    public static int GetTutorialLevel() {
        return PlayerPrefs.GetInt("tutorialLevel", 0);
    } 
    public static bool GetIfTutorial() {
        return PlayerPrefs.GetInt("isTutorial",0) == 0? false : true;
    }
    public static void SetIfTutorial(int index = -1) {
        if(index == -1)
            PlayerPrefs.SetInt("isTutorial", 0);
        else
            PlayerPrefs.SetInt("isTutorial", 1);

        PlayerPrefs.SetInt("tutorialLevel", index);
    } 
    public void SetLevel(int value) { 
        PlayerPrefs.SetInt("activeLevel", value);
        activeLevelIndex = GetActiveLevel();
    }
    public void NexlLevel() {
        if (GetIfTutorial() == true)
            return;
        PlayerPrefs.SetInt("activeLevel", GetActiveLevel() + 1);
        activeLevelIndex = GetActiveLevel();
    }
}
[System.Serializable]
public class LevelSetting {
    public string Name;
    [TextArea]
    public string Description;
    [Space(10f)] 
    public DungeonLevel DungeonLevel;
    [Space(10f)] 
    public TutorialLevel TutorialLevel;
}
[System.Serializable]
public struct DungeonLevel {
    public bool IsRandomDungeon;
    [Header("PDG_SimpleRandomWalk")]
    public SimpleRandomWalkData SRWData;
    [Header("PDG_CorridorFirst")]
    public int CorridorLength;
    public int CorridorCount;
    [Range(0.1f, 1f)]
    public float RoomPercent;
    [Header("PDG_Rooms")]
    public PDG_RoomProps RoomProps;
    [Header("TilemapVisualizerData")]
    public TilemapVisualizerData TilemapVisualizerData;
}
[System.Serializable]
public struct TutorialLevel {
    public bool IsTutorial;
    public GameObject TutorialMap;
}