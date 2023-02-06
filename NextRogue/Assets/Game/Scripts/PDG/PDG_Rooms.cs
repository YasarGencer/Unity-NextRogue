using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

public class PDG_Rooms : MonoBehaviour
{
    private List<HashSet<Vector2Int>> _floorPoses = new();
    private List<IEnumerable<Vector2Int>> _corridorPoses = new();

    public void Initialize() {
        CreateRooms();
    }
    public void SaveRoom(HashSet<Vector2Int> item) { _floorPoses.Add(item); }
    public void SaveCorridor(IEnumerable<Vector2Int> item) { _corridorPoses.Add(item); }
    public void CreateRooms() {
        HashSet<Vector2Int> allTiles = new HashSet<Vector2Int>();
        foreach (var item in _corridorPoses) {
            MainManager.Instance.PDGManager.TilemapVisualizer.PaintFloorTiles(item);
            allTiles.UnionWith(item);
        }
        foreach (var item in _floorPoses) {
            MainManager.Instance.PDGManager.TilemapVisualizer.PaintFloorTiles(item);
            allTiles.UnionWith(item);
        }

        WallGenerator.CreateWalls(allTiles, MainManager.Instance.PDGManager.TilemapVisualizer);
    }
    public void ColorizeRooms() {
        MainManager.Instance.PDGManager.TilemapVisualizer.PaintDefaulToFloor(_corridorPoses[0]);
    }

}
