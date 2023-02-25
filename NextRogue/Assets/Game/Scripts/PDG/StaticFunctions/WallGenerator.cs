using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class WallGenerator 
{
    public static HashSet<Vector2Int> CreateWalls(HashSet<Vector2Int> floorPeses, TilemapVisualizer tilemapVisualizer) {
        var basicWallPoses = FindWallsInDirections(floorPeses, Direction2D.CardinalDirectionList);
        var cornerWallPoses = FindWallsInDirections(floorPeses, Direction2D.DiagonalDirectionList);

        CreateBasicWall(tilemapVisualizer, basicWallPoses, floorPeses);
        CreateCornerWall(tilemapVisualizer, cornerWallPoses, floorPeses);

        var allWallPoses = new HashSet<Vector2Int>();
        allWallPoses.UnionWith(basicWallPoses);
        allWallPoses.UnionWith(cornerWallPoses);
        return allWallPoses;
    }

    private static void CreateCornerWall(TilemapVisualizer tilemapVisualizer, HashSet<Vector2Int> cornerWallPoses, HashSet<Vector2Int> floorPeses) {
        foreach (var pos in cornerWallPoses) {
            string neighbourBinaryType = "";
            foreach (var direction in Direction2D.EightDirectionList) {
                var neighbourPos = pos + direction;
                if (floorPeses.Contains(neighbourPos))
                    neighbourBinaryType += "1";
                else
                    neighbourBinaryType += "0";
            }
            tilemapVisualizer.PaintSingleCornerWall(pos, neighbourBinaryType);
        }
    }

    private static void CreateBasicWall(TilemapVisualizer tilemapVisualizer, HashSet<Vector2Int> basicWallPoses, HashSet<Vector2Int> floorPeses) {
        foreach (var pos in basicWallPoses) {
            string neighbourBinaryType = "";
            foreach (var direction in Direction2D.CardinalDirectionList) {
                var neighbourPos = pos + direction;
                if (floorPeses.Contains(neighbourPos))
                    neighbourBinaryType += "1";
                else
                    neighbourBinaryType += "0";
            }
            tilemapVisualizer.PaintSingleBasicWall(pos, neighbourBinaryType);
        }    
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
