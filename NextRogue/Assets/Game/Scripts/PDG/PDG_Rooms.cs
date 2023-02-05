using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PDG_Rooms : MonoBehaviour
{
    private List<HashSet<Vector2Int>> _floorPoses;
    private List<HashSet<Vector2Int>> _corridorPoses;

    public void Initialize() {
        _floorPoses = new();
        _corridorPoses = new();
    }
    public void SaveRoom(HashSet<Vector2Int> item) { _floorPoses.Add(item); }
    public void SaveCorridor(HashSet<Vector2Int> item) { _corridorPoses.Add(item); }
}
