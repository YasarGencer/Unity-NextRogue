using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ADungeonGenerator : MonoBehaviour {

    [SerializeField]
    protected TilemapVisualizer _tilemapVisualizer;
    [SerializeField]
    protected Vector2Int _startPos = Vector2Int.zero;
    public void GenerateDungeon() {
        ClearDungeon();
        RunProceduralGeneration();
    }
    public void ClearDungeon() {
        _tilemapVisualizer.Clear();
    }
    protected abstract void RunProceduralGeneration();
}
