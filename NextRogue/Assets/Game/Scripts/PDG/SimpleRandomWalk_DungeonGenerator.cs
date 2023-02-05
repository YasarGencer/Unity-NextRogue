using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SimpleRandomWalk_DungeonGenerator : ADungeonGenerator
{
    [SerializeField]
    protected SimpleRandomWalkData _SRWData;

    private void Start() {
        RunProceduralGeneration();
    }
    protected override void RunProceduralGeneration() {
        HashSet<Vector2Int> floorPoses = RunRandomWalk(_SRWData, _startPos);
        _tilemapVisualizer.PaintFloorTiles(floorPoses);
        WallGenerator.CreateWalls(floorPoses, _tilemapVisualizer);
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
        return floorPeses;
    }
}
