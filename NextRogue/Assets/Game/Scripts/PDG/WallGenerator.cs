using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WallGenerator 
{
    public static void CreateWalls(HashSet<Vector2Int> floorPeses, TilemapVisualizer tilemapVisualizer) {
        var basicWallPoses = FindWallsInDirections(floorPeses, Direction2D.DirectionList);
        tilemapVisualizer.PaintTiles(basicWallPoses, tilemapVisualizer.WallTilemap, tilemapVisualizer.WallTile);
    }

    private static HashSet<Vector2Int> FindWallsInDirections(HashSet<Vector2Int> floorPeses, List<Vector2Int> directionList) {
        HashSet<Vector2Int> wallPoses = new();
        foreach (var pos in floorPeses) {
            foreach (var direction in directionList) {
                var neighbourPos = pos + direction;
                if(floorPeses.Contains(neighbourPos) == false)
                    wallPoses.Add(neighbourPos);
            }
        }
        return wallPoses;
    }
}
