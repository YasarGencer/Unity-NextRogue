using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PDG_SimpleRandomWalk : APDG
{  
    public override void Initialize() {
        base.Initialize();
    }
    protected override void RunProceduralGeneration() {
        HashSet<Vector2Int> floorPoses = RunRandomWalk(MainManager.Instance.LevelManager.ActiveLevelSetting.DungeonLevel.SRWData, _startPos);


        MainManager.Instance.LevelManager.PDGManager.TilemapVisualizer.PaintFloorTiles(floorPoses);
        WallGenerator.CreateWalls(floorPoses, MainManager.Instance.LevelManager.PDGManager.TilemapVisualizer);

        MainManager.Instance.LevelManager.PDGManager.Rooms.SaveRoom(floorPoses);
    }

    protected HashSet<Vector2Int> RunRandomWalk(SimpleRandomWalkData sRWData, Vector2Int pos) {
        var currentPos = pos;
        HashSet<Vector2Int> floorPeses = new();
        for (int i = 0; i < sRWData.Iterations; i++) {
            var path = PDGAlgorithms.SimpleRandomWalk(currentPos, sRWData.WalkLength);
            floorPeses.UnionWith(path);
            if (sRWData.StartRandomly) {
                currentPos = floorPeses.ElementAt(UnityEngine.Random.Range(0, floorPeses.Count));           
            }
        }

        MainManager.Instance.LevelManager.PDGManager.Rooms.SaveRoom(floorPeses);
        return floorPeses;
    }
}
