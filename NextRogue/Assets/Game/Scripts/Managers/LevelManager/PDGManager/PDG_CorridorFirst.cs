using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PDG_CorridorFirst : PDG_SimpleRandomWalk
{ 

    public override void Initialize() {
        base.Initialize();
    }
    protected override void RunProceduralGeneration() {
        CorridorFirstGeneration();
    }

    private void CorridorFirstGeneration() {
        HashSet<Vector2Int> floorPoses = new();
        HashSet<Vector2Int> potentialRoomPoses = new();

        CreateCorridors(floorPoses, potentialRoomPoses);


        HashSet<Vector2Int> roomPoses = CreateRooms(potentialRoomPoses);

        List<Vector2Int> deadEnds = FindAllDeadEnds(floorPoses);

        CreateRoomsAtDeadEnds(deadEnds, roomPoses);

        floorPoses.UnionWith(roomPoses);


        //MainManager.Instance.LevelManager.PDGManager.TilemapVisualizer.PaintFloorTiles(floorPoses);
        //WallGenerator.CreateWalls(floorPoses, MainManager.Instance.LevelManager.PDGManager.TilemapVisualizer);
    }

    private void CreateRoomsAtDeadEnds(List<Vector2Int> deadEnds, HashSet<Vector2Int> roomFloors) {
        foreach (var pos in deadEnds) {
            if(roomFloors.Contains(pos) == false) {
                var room = RunRandomWalk(MainManager.Instance.LevelManager.ActiveLevelSetting.DungeonLevel.SRWData, pos);
                roomFloors.UnionWith(room);
            }
        }
    }

    private List<Vector2Int> FindAllDeadEnds(HashSet<Vector2Int> floorPoses) {
        List<Vector2Int> deadEnds = new();
        foreach (var pos in floorPoses) {
            int neighboursCount = 0;
            foreach (var direction in Direction2D.CardinalDirectionList) {
                if (floorPoses.Contains(pos + direction))
                    neighboursCount++;
            }
            if (neighboursCount == 1)
                deadEnds.Add(pos);
        }
        return deadEnds;
    }

    private HashSet<Vector2Int> CreateRooms(HashSet<Vector2Int> potentialRoomPoses) {
        HashSet<Vector2Int> roomPoses = new();
        int roomToCreateCount = (int)MathF.Round(potentialRoomPoses.Count * MainManager.Instance.LevelManager.ActiveLevelSetting.DungeonLevel.RoomPercent);

        List<Vector2Int> roomsToCreate = potentialRoomPoses.OrderBy(x => Guid.NewGuid()).Take(roomToCreateCount).ToList();
        
        if (!roomsToCreate.Contains(_startPos))
            roomsToCreate.Add(_startPos);

        foreach (var roomPos in roomsToCreate) {
            var roomFloor = RunRandomWalk(MainManager.Instance.LevelManager.ActiveLevelSetting.DungeonLevel.SRWData, roomPos);

            roomPoses.UnionWith(roomFloor);
        }
        return roomPoses;
    }

    private void CreateCorridors(HashSet<Vector2Int> floorPoses, HashSet<Vector2Int> potentialRoomPoses) {
        var currentPos = _startPos;
        potentialRoomPoses.Add(currentPos);
        for (int i = 0; i < MainManager.Instance.LevelManager.ActiveLevelSetting.DungeonLevel.CorridorCount; i++) {
            var corridor = PDGAlgorithms.RandomWalkCorridor(currentPos, 
                MainManager.Instance.LevelManager.ActiveLevelSetting.DungeonLevel.CorridorLength, 
                MainManager.Instance.LevelManager.ActiveLevelSetting.DungeonLevel.SRWData.isRandomizedDirections);

            MainManager.Instance.LevelManager.PDGManager.Rooms.SaveCorridor(corridor);
            

            currentPos = corridor[corridor.Count - 1];

            potentialRoomPoses.Add(currentPos);

            floorPoses.UnionWith(corridor);
        }
    }

}
