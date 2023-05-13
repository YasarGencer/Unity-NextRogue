using UnityEngine;

public abstract class APDG : MonoBehaviour { 
    protected Vector2Int _startPos = Vector2Int.zero;
    public virtual void Initialize() {
        GenerateDungeon();
    }
    public void GenerateDungeon() {
        RunProceduralGeneration();
    }
    protected abstract void RunProceduralGeneration();
}
