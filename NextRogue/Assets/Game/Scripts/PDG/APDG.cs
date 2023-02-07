using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public abstract class APDG : MonoBehaviour {
    [Space(10f)]
    [SerializeField]
    protected Vector2Int _startPos = Vector2Int.zero;
    public virtual void Initialize() {
        GenerateDungeon();
    }
    public void GenerateDungeon() {
        RunProceduralGeneration();
    }
    protected abstract void RunProceduralGeneration();
}
