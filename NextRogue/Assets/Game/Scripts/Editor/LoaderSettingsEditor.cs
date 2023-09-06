using UnityEditor;
using UnityEditor.TerrainTools;
using UnityEngine;

[CustomEditor(typeof(LoaderElement))]
public class LoaderSettingsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        LoaderElement loaderSettings = (LoaderElement)target;


        EditorGUILayout.BeginHorizontal();
        
        EditorGUILayout.LabelField(loaderSettings.name, EditorStyles.boldLabel);
        if (GUILayout.Button("Save", GUILayout.Width(100), GUILayout.ExpandWidth(false)))
        {
            for (int j = 0; j < loaderSettings.loadingTexts.Count; j++)
            {
                if (loaderSettings.loadingTexts[j] == "")
                {
                    loaderSettings.loadingTexts.RemoveAt(j);
                    j--;
                }
            }
            EditorUtility.SetDirty(loaderSettings);
            AssetDatabase.SaveAssets();
        }
        EditorGUILayout.EndHorizontal();


        EditorGUILayout.BeginVertical("box");
        EditorGUILayout.BeginHorizontal();
        loaderSettings.loadingSprite = (Sprite)EditorGUILayout.ObjectField("Loading Sprite:", loaderSettings.loadingSprite, typeof(Sprite), false);
        EditorGUILayout.EndHorizontal();

        loaderSettings.textColor = EditorGUILayout.ColorField("Text Color:",loaderSettings.textColor);
        loaderSettings.barColor = EditorGUILayout.ColorField("Bar Color:",loaderSettings.barColor);
        

        GUILayout.Label("Loading Texts:");

        for (int j = 0; j < loaderSettings.loadingTexts.Count; j++)
        {
            EditorGUILayout.BeginHorizontal();

            loaderSettings.loadingTexts[j] = EditorGUILayout.TextArea(loaderSettings.loadingTexts[j], GUILayout.Height(EditorGUIUtility.singleLineHeight * 2), GUILayout.MaxWidth(EditorGUIUtility.currentViewWidth - 85));
            if (GUILayout.Button("-", GUILayout.Width(40), GUILayout.Height(EditorGUIUtility.singleLineHeight * 2)))
            {
                loaderSettings.loadingTexts.RemoveAt(j);
                j--;
            }

            EditorGUILayout.EndHorizontal();
        }
        if (GUILayout.Button("Add Text"))
        {
            loaderSettings.loadingTexts.Add("");
        }


        EditorGUILayout.EndVertical();
    }
}