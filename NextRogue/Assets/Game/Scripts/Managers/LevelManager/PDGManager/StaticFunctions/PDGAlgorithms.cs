using System.Collections.Generic;
using UnityEngine;

public static class PDGAlgorithms
{
    public static HashSet<Vector2Int> SimpleRandomWalk(Vector2Int startPos, int walkLength) {
        HashSet<Vector2Int> path = new();
        //starts from the first position
        path.Add(startPos);
        var prevPos = startPos;
        //goes to the random direction 1 block
        for (int i = 0; i < walkLength; i++) {
            var newPos = prevPos + Direction2D.GetRandomCardinalDirection();
            path.Add(newPos);
            prevPos = newPos;
        }
        return path;
    }
    public static List<Vector2Int> RandomWalkCorridor(Vector2Int startPos, int corridorLength, bool isRandomDirections) {
        List<Vector2Int> corridor = new();
        Vector2Int direction = Vector2Int.right;
        if (isRandomDirections)
            direction = Direction2D.GetRandomCardinalDirection();
        var currentPos = startPos;
        corridor.Add(currentPos);
        for (int i = 0; i < corridorLength; i++) {
            currentPos += direction;
            corridor.Add(currentPos);
        }
        return corridor;
    }
}

public static class Direction2D{
    public static List<Vector2Int> CardinalDirectionList = new List<Vector2Int>{

        new Vector2Int(0,1), //UP
        new Vector2Int(1,0), //RIGHT
        new Vector2Int(0, -1), // DOWN
        new Vector2Int(-1, 0) //LEFT
    };
    public static Vector2Int GetRandomCardinalDirection() {
        return CardinalDirectionList[new System.Random().Next(0, CardinalDirectionList.Count)];
    }
    public static List<Vector2Int> DiagonalDirectionList = new List<Vector2Int>{
        new Vector2Int(1,1), //UP-RIGHT
        new Vector2Int(1,-1), //RIGHT-DOWN
        new Vector2Int(-1, -1), // DOWN-LEFT
        new Vector2Int(-1, 1) //LEFT-UP
    };
    public static Vector2Int GetRandomDiagonalDirection() {
        return DiagonalDirectionList[new System.Random().Next(0, DiagonalDirectionList.Count)];
    }
    public static List<Vector2Int> EightDirectionList = new List<Vector2Int>{

        new Vector2Int(0,1), //UP
        new Vector2Int(1,1), //UP-RIGHT
        new Vector2Int(1,0), //RIGHT
        new Vector2Int(1,-1), //RIGHT-DOWN
        new Vector2Int(0, -1), // DOWN
        new Vector2Int(-1, -1), // DOWN-LEFT
        new Vector2Int(-1, 0), //LEFT
        new Vector2Int(-1, 1) //LEFT-UP
    };
    public static Vector2Int GetRandomEightDirection() {
        return EightDirectionList[new System.Random().Next(0, EightDirectionList.Count)];
    }
}