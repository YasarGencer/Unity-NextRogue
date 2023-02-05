using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ADungeonGenerator : MonoBehaviour {

    protected PDGManager _manager;
    [SerializeField]
    protected Vector2Int _startPos = Vector2Int.zero;
    public virtual void Initialize(PDGManager manager) {
        _manager = manager;
        GenerateDungeon();
    }
    public void GenerateDungeon() {
        ClearDungeon();
        RunProceduralGeneration();
    }
    public void ClearDungeon() {
        _manager.TilemapVisualizer.Clear();
    }
    protected abstract void RunProceduralGeneration();
}
