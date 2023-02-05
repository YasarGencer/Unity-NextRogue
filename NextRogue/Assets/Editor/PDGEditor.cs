using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ADungeonGenerator), true)]
public class PDGEditor : Editor {
    ADungeonGenerator generator;

    private void Awake() {
        generator= (ADungeonGenerator)target;
    }
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        if (GUILayout.Button("Create Dungon")) {
            generator.GenerateDungeon();
        }
        if (GUILayout.Button("Clear Dungon")) {
            generator.ClearDungeon();
        }
    }
}
