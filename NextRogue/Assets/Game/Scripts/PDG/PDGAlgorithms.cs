using System.Collections;
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
            var newPos = prevPos + Direction2D.GetRandomDirection();
            path.Add(newPos);
            prevPos = newPos;
        }
        return path;
    }
    public static List<Vector2Int> RandomWalkCorridor(Vector2Int startPos, int corridorLength) {
        List<Vector2Int> corridor = new();
        var direction = Direction2D.GetRandomDirection();
        var currentPos = startPos;
        corridor.Add(currentPos);
        for (int i = 0; i < corridorLength; i++) {
            currentPos += direction;
            corridor.Add(currentPos);
        }
        return corridor;
    }
}

public static class Direction2D {
    public static List<Vector2Int> DirectionList = new List<Vector2Int>{
        new Vector2Int(0, 1),
        new Vector2Int(1, 0),
        new Vector2Int(0, -1),
        new Vector2Int(-1, 0),
    };
    public static Vector2Int GetRandomDirection() {
        return DirectionList[Random.Range(0, DirectionList.Count)];
    }
}
