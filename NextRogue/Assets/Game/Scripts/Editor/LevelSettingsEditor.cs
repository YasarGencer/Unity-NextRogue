using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LevelSettings))]
public class LevelSettingsEditor : Editor
{
    private bool showLevels = true;
    private int removeThis = -1;

    public override void OnInspectorGUI()
    {
        LevelSettings levelSettings = (LevelSettings)target;

        EditorGUILayout.Space(5);

        GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);
        buttonStyle.fontSize = 24;
        buttonStyle.fixedHeight = 40;
        buttonStyle.fontStyle = FontStyle.Bold;
        buttonStyle.normal.textColor = Color.white;
        buttonStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn left.png") as Texture2D;

        if (GUILayout.Button("Save", buttonStyle))
        {
            EditorUtility.SetDirty(levelSettings);
            AssetDatabase.SaveAssets();
        }

        EditorGUILayout.Space(10);

        GUIStyle headerStyle = new GUIStyle(EditorStyles.boldLabel);
        headerStyle.fontSize = 16;
        headerStyle.alignment = TextAnchor.MiddleCenter;

        EditorGUILayout.BeginVertical();

        GUILayout.Label("Level Settings", headerStyle);

        EditorGUILayout.Space(10);

        EditorGUILayout.BeginHorizontal();

        GUILayout.FlexibleSpace();

        if (GUILayout.Button("Edit Levels", showLevels ? SelectedButton() : UnSelectedButton()))
        {
            showLevels = true;
        }

        if (GUILayout.Button("Edit Tutorial Levels", showLevels ? UnSelectedButton() : SelectedButton()))
        {
            showLevels = false;
        }

        GUILayout.FlexibleSpace();

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space(20);

        EditorGUILayout.EndVertical();

        if (showLevels)
        {
            ShowLevels(levelSettings.Levels, "Levels");
        }
        else
        {
            ShowLevels(levelSettings.TutorialLevels, "Tutorial Levels");
        }

        EditorGUILayout.Space(10);
    }
    private GUIStyle SelectedButton()
    {
        GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);
        buttonStyle.fontSize = 12;
        buttonStyle.fixedWidth = 150;

        buttonStyle.normal.textColor = Color.white;
        buttonStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn left.png") as Texture2D;

        return buttonStyle;
    }
    private GUIStyle UnSelectedButton()
    {
        GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);
        buttonStyle.fontSize = 12;
        buttonStyle.fixedWidth = 150;

        buttonStyle.normal.textColor = Color.black;
        buttonStyle.normal.background = null;

        return buttonStyle;
    }
    private void ShowLevels(List<LevelSetting> levels, string header)
    {
        GUIStyle headerStyle = new GUIStyle(EditorStyles.boldLabel);
        headerStyle.fontSize = 16;
        headerStyle.alignment = TextAnchor.MiddleCenter;
        headerStyle.normal.textColor = Color.white;

        GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);
        buttonStyle.fontSize = 12;
        buttonStyle.normal.textColor = Color.white;
        buttonStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn left.png") as Texture2D;
        
        GUIStyle boxStyle = new GUIStyle("box");
        boxStyle.padding = new RectOffset(10, 10, 10, 10);

        GUILayout.BeginVertical(boxStyle);

        GUILayout.Space(5);
        GUILayout.Label(header, headerStyle);
        GUILayout.Space(10);

        for (int i = 0; i < levels.Count; i++)
        {
            LevelSetting currentLevel = levels[i];

            GUIStyle itemStyle = new GUIStyle("HelpBox");
            itemStyle.padding = new RectOffset(10, 10, 10, 10);

            EditorGUILayout.BeginVertical(itemStyle);

            GUIStyle levelHeaderStyle = new GUIStyle(EditorStyles.boldLabel);
            levelHeaderStyle.fontSize = 14;
            levelHeaderStyle.normal.textColor = Color.white;
            levelHeaderStyle.fixedWidth = 300;

            EditorGUILayout.BeginHorizontal();
            currentLevel.showme = EditorGUILayout.Foldout(currentLevel.showme, "  " + currentLevel.Name, true, levelHeaderStyle);
            EditorGUILayout.EndHorizontal();

            if (currentLevel.showme)
            {
                GUILayout.Space(10);

                currentLevel.Name = EditorGUILayout.TextField("Name", currentLevel.Name);

                EditorGUILayout.LabelField("Desription");
                currentLevel.Description = EditorGUILayout.TextArea(currentLevel.Description, GUILayout.Height(EditorGUIUtility.singleLineHeight * 2));

                currentLevel.CameraLens = EditorGUILayout.FloatField("Camera Lens", currentLevel.CameraLens); 

                EditorGUILayout.Space(5);
                currentLevel.Loader = (LoaderElement)EditorGUILayout.ObjectField("Loader", currentLevel.Loader, typeof(LoaderElement), true);

                EditorGUILayout.Space(5);

                #region [Level Type]
                EditorGUILayout.BeginHorizontal();

                Color backgroundCol = new Color(0.2f, 0.2f, 0.2f, 1.0f);
                Texture2D backgroundTex = new Texture2D(1, 1);
                backgroundTex.SetPixel(0, 0, backgroundCol);
                backgroundTex.Apply();

                GUIStyle enumPopupStyle = new GUIStyle(EditorStyles.popup);
                enumPopupStyle.fixedHeight = 18;
                enumPopupStyle.margin = new RectOffset(0, 0, 2, 0);
                enumPopupStyle.padding = new RectOffset(4, 4, 0, 0);
                enumPopupStyle.normal.textColor = Color.white;
                enumPopupStyle.normal.background = backgroundTex;

                currentLevel.MyType = (DisplayOption)EditorGUILayout.EnumPopup("Level Type", currentLevel.MyType, enumPopupStyle);

                EditorGUILayout.EndHorizontal();
                #endregion

                EditorGUILayout.Space(15);

                GUIStyle sectionLabelStyle = new GUIStyle(EditorStyles.boldLabel);
                sectionLabelStyle.fontSize = 14;
                sectionLabelStyle.alignment = TextAnchor.MiddleCenter;
                sectionLabelStyle.normal.textColor = Color.white;

                EditorGUILayout.LabelField(currentLevel.MyType.ToString() + " Properties", sectionLabelStyle);

                GUILayout.Space(5);

                if (currentLevel.MyType == DisplayOption.Dungeon)
                {
                    currentLevel.DungeonLevel = ShowDungeonProperties(currentLevel.DungeonLevel);
                }
                else if (currentLevel.MyType == DisplayOption.Shop)
                {
                    currentLevel.ShopLevel = ShowShopProperties(currentLevel.ShopLevel);
                }
                else if (currentLevel.MyType == DisplayOption.Tutorial)
                {
                    currentLevel.TutorialLevel = ShowTutorialProperties(currentLevel.TutorialLevel);
                } else if(currentLevel.MyType == DisplayOption.Boss) {
                    currentLevel.BossLevel = ShowBossProperties(currentLevel.BossLevel);
                }

                GUILayout.Space(10);

                if (GUILayout.Button("Remove", buttonStyle, GUILayout.ExpandWidth(false)))
                {
                    removeThis = i;
                }

            }
            
            EditorGUILayout.EndVertical();
            GUILayout.Space(10);
        }

        if (GUILayout.Button("Add Level", buttonStyle))
        {
            levels.Add(new LevelSetting() { Name = "New Level" });
        }

        GUILayout.EndVertical();

        if (removeThis != -1)
        {
            int RemoveThis = removeThis;
            removeThis = -1;

            if (EditorUtility.DisplayDialog("Confirm Deletion", "Are you sure you want to delete this level?", "Yes", "No"))
            {
                levels.RemoveAt(RemoveThis);
            }
        }
    }
    private DungeonLevel ShowDungeonProperties(DungeonLevel dungeonLevel)
    {
        GUIStyle propertyLabelStyle = new GUIStyle(EditorStyles.boldLabel);
        propertyLabelStyle.fontSize = 14;

        GUILayout.Space(10);

        EditorGUILayout.BeginVertical("box");

        EditorGUILayout.LabelField("PDG_SimpleRandomWalk", propertyLabelStyle);

        EditorGUI.indentLevel++;

        dungeonLevel.SRWData = (SimpleRandomWalkData)EditorGUILayout.ObjectField("SRW Data", dungeonLevel.SRWData, typeof(SimpleRandomWalkData), true);

        EditorGUI.indentLevel--;

        EditorGUILayout.EndVertical();

        EditorGUILayout.Space(5);
        EditorGUILayout.BeginVertical("box");
        EditorGUILayout.LabelField("PDG_CorridorFirst", propertyLabelStyle);

        EditorGUI.indentLevel++;

        dungeonLevel.CorridorLength = EditorGUILayout.IntField("Corridor Length", dungeonLevel.CorridorLength);
        dungeonLevel.CorridorCount = EditorGUILayout.IntField("Corridor Count", dungeonLevel.CorridorCount);
        dungeonLevel.RoomPercent = EditorGUILayout.Slider("Room Percent", dungeonLevel.RoomPercent, 0.1f, 1f);

        EditorGUI.indentLevel--;

        EditorGUILayout.EndVertical();

        dungeonLevel.Difficulty = EditorGUILayout.Slider("Difficulty", dungeonLevel.Difficulty, 0.5f, 5f);

        EditorGUILayout.Space(5);
        EditorGUILayout.BeginVertical("box");
        EditorGUILayout.LabelField("PDG_Rooms", propertyLabelStyle);

        EditorGUI.indentLevel++;

        dungeonLevel.RoomProps = (PDG_RoomProps)EditorGUILayout.ObjectField("Room Props", dungeonLevel.RoomProps, typeof(PDG_RoomProps), true);

        EditorGUI.indentLevel--;

        EditorGUILayout.EndVertical();

        EditorGUILayout.Space(5);
        EditorGUILayout.BeginVertical("box");
        EditorGUILayout.LabelField("TilemapVisualizerData", propertyLabelStyle);

        EditorGUI.indentLevel++;

        dungeonLevel.TilemapVisualizerData = (TilemapVisualizerData)EditorGUILayout.ObjectField("Tilemap Visualizer Data", dungeonLevel.TilemapVisualizerData, typeof(TilemapVisualizerData), true);

        EditorGUI.indentLevel--;

        EditorGUILayout.EndVertical();
        return dungeonLevel;
    }
    private ShopLevel ShowShopProperties(ShopLevel shopLevel)
    {
        GUIStyle propertyLabelStyle = new GUIStyle(EditorStyles.boldLabel);
        propertyLabelStyle.fontSize = 14;

        GUILayout.Space(10);

        EditorGUILayout.BeginVertical("box");

        EditorGUILayout.LabelField("ShopMap", propertyLabelStyle);

        EditorGUI.indentLevel++;

        shopLevel.ShopMap = (GameObject)EditorGUILayout.ObjectField("Shop Map", shopLevel.ShopMap, typeof(GameObject), true);

        EditorGUI.indentLevel--;

        EditorGUILayout.EndVertical();
        return shopLevel;
    }
    private TutorialLevel ShowTutorialProperties(TutorialLevel tutorialLevel)
    {
        GUIStyle propertyLabelStyle = new GUIStyle(EditorStyles.boldLabel);
        propertyLabelStyle.fontSize = 14;

        GUILayout.Space(10);

        EditorGUILayout.BeginVertical("box");

        EditorGUILayout.LabelField("TutorialMap", propertyLabelStyle);

        EditorGUI.indentLevel++;

        tutorialLevel.TutorialMap = (GameObject)EditorGUILayout.ObjectField("Tutorial Map", tutorialLevel.TutorialMap, typeof(GameObject), true);

        EditorGUI.indentLevel--;

        EditorGUILayout.EndVertical();
        return tutorialLevel;
    }
    private BossLevel ShowBossProperties(BossLevel bossLevel) {
        GUIStyle propertyLabelStyle = new GUIStyle(EditorStyles.boldLabel);
        propertyLabelStyle.fontSize = 14;

        GUILayout.Space(10);

        EditorGUILayout.BeginVertical("box");

        EditorGUILayout.LabelField("BossMap", propertyLabelStyle);

        EditorGUI.indentLevel++;

        bossLevel.BossMap = (GameObject)EditorGUILayout.ObjectField("Boss Map", bossLevel.BossMap, typeof(GameObject), true);

        EditorGUI.indentLevel--;

        EditorGUILayout.EndVertical();
        return bossLevel;
    }

}
