using System.Collections.Generic;
using System.Linq; 
using UnityEngine;

public class PDG_Rooms : MonoBehaviour
{
    private List<HashSet<Vector2Int>> _floorPoses;
    private List<IEnumerable<Vector2Int>> _corridorPoses; 
    public HashSet<Vector2Int> AllCorridors { get; private set; }
    public HashSet<Vector2Int> AllWalls { get; private set; }
    [SerializeField]
    private List<Room> _rooms; 

    public void Initialize() {
        MainManager.Instance.LevelManager.PDGManager.TilemapVisualizer
            .SetTVData(MainManager.Instance.LevelManager.ActiveLevelSetting.DungeonLevel.TilemapVisualizerData);
        GameObject.Find("DungeonBG").GetComponent<SpriteRenderer>().color = MainManager.Instance.LevelManager.ActiveLevelSetting.DungeonLevel.TilemapVisualizerData.BGColor;
        CreateRooms();
        SortRooms();
        RoomTypes();
        DecorateRooms(); 
    }
    public void SaveRoom(HashSet<Vector2Int> item) { _floorPoses.Add(item); }
    public void SaveCorridor(IEnumerable<Vector2Int> item) { _corridorPoses.Add(item); }
    public void ResetValues() {    
        _floorPoses = new();
        _corridorPoses = new(); 
        AllCorridors = new();
        AllWalls = new();
        _rooms = new();
    }
    public void CreateRooms() {
        HashSet<Vector2Int> allTiles = new HashSet<Vector2Int>();

        foreach (var item in _corridorPoses) {
            allTiles.UnionWith(item);
            AllCorridors.UnionWith(item);
        }
        foreach (var item in _floorPoses) {
            allTiles.UnionWith(item); 
        }
        GameObject.FindObjectOfType<MapCamera>().GetTiles(allTiles);
        MainManager.Instance.LevelManager.PDGManager.TilemapVisualizer.PaintFloorTiles(allTiles);
        AllWalls = WallGenerator.CreateWalls(allTiles, MainManager.Instance.LevelManager.PDGManager.TilemapVisualizer); 
        for (int i = 0; i < _floorPoses.Count; i++) {
            if(_corridorPoses.Count > i)
                _rooms.Add(new(_floorPoses[i], _corridorPoses[i]));
            else
                _rooms.Add(new(_floorPoses[i], _corridorPoses[0]));
        }
    }
    public void SortRooms() {
        _rooms = _rooms.OrderBy(x => x.Distance).ToList();
        for (int i = 0; i < _rooms.Count; i++)
            _rooms[i].ID = i;
    }
    public void RoomTypes() { 
        _rooms[0].RoomType = RoomType.Start;
        _rooms[_rooms.Count - 1].RoomType = RoomType.Exit;
        //_rooms[Random.Range(2, _rooms.Count - 1)].RoomType = RoomType.Treasure;
    }
    public void DecorateRooms() {
        PDG_RoomProps room_props = MainManager.Instance.LevelManager.ActiveLevelSetting.DungeonLevel.RoomProps;
        foreach (var item in _rooms) {
            item.DecorateWalls(room_props.WallProps, AllWalls);
            switch (item.RoomType) {
                case RoomType.Start:
                    item.DecorateStartRoom(room_props.StartRoom);
                    break;
                case RoomType.Enemy: 
                    item.DecorateCenter(room_props.CenterProps, AllWalls);
                    item.DecorateEnemyRoom(room_props.EnemyRoom);
                    break;
                case RoomType.Treasure: 
                    break;
                case RoomType.Key:
                    break;
                case RoomType.Exit:
                    item.DecorateExitRoom(room_props.ExitRoom);
                    break;
                case RoomType.Boss:
                    break;
                default:
                    break;
            }

        }
        GameObject.FindObjectOfType<MapCamera>().GetGameObjects();
    }
}
[System.Serializable]
public class Room {
    public int ID;
    public HashSet<Vector2Int> Floor;
    public IEnumerable<Vector2Int> Corridor;
    public Vector2Int Center;
    public RoomType RoomType;
    public float Distance;
    private List<Vector2Int> _usedPos;

    public Room(HashSet<Vector2Int> floor, IEnumerable<Vector2Int> corridor) {
        Floor = floor;
        Corridor = corridor;
        foreach (var poses in Floor) {
            Center = poses;
            break;
        }
        RoomType = RoomType.Enemy;
        Distance = Vector2.Distance(Vector2.zero, Center);
        _usedPos = new();
    }
    public void DecorateEnemyRoom(List<GameObject> props) {
        if (props.Count <= 0)
            return;
        int enemyCount = Random.Range(5, 10);
        for (int i = 0; i < enemyCount; i++) {

            NP_MainController mainController = GameObject.Instantiate(
                props[Random.Range(0, props.Count)],
                MainManager.Instance.Enemies
                ).GetComponent<NP_MainController>();

            mainController.transform.position = new Vector3(Center.x, Center.y, 0);
            mainController.Initialize();
        }
    }
    public void DecorateExitRoom(List<GameObject> props) {
        if (props.Count <= 0)
            return;
        foreach (var item in props) {
            GameObject.Instantiate(
                props[Random.Range(0, props.Count)],
                MainManager.Instance.Environment
                ).transform.position = new(Center.x, Center.y, 0);
        }
    }
    public void DecorateStartRoom(List<GameObject> props) {
        if (props.Count <= 0)
            return;
        foreach (var item in props) {
            GameObject.Instantiate(
                props[Random.Range(0, props.Count)],
                MainManager.Instance.Environment
                ).transform.position = new(Center.x, Center.y, 0);
        }
    }
    public void DecorateCenter(List<GameObject> props, HashSet<Vector2Int> walls) {
        if (props.Count <= 0)
            return; 
        if (props.Count != 1) {
            int propCount = Random.Range(3, 7);
            int activeCount = 0;
            while (propCount > activeCount) {
                bool decorate = true;

                //do not spawn props on top of each other
                Vector2Int pos = new(0, 0);
                do
                    pos = Floor.ElementAt(Random.Range(0, Floor.Count));
                while (_usedPos.Contains(pos) || Corridor.Contains(pos));

                //check if current position is beside walls
                foreach (var item in Direction2D.EightDirectionList) {
                    if (walls.Contains(pos + item)) {
                        decorate = false;
                        break;
                    }
                }
                //if not
                if (decorate) {
                    foreach (var item in Direction2D.CardinalDirectionList)
                        if (Floor.Contains(pos + item * 2))
                            decorate = false;
                        else {
                            decorate = true;
                            break;
                        }
                }
                //spawn prop
                if (decorate) {
                    GameObject.Instantiate(props[Random.Range(0, props.Count)], new Vector3(pos.x, pos.y, 0), Quaternion.identity)
                        .transform.parent = MainManager.Instance.Environment;
                    activeCount++;
                    _usedPos.Add(pos);
                }
            }
        }
        else if (props.Count == 1) {
            Vector2Int pos = Center;
            GameObject.Instantiate(props[0], new Vector3(pos.x, pos.y, 0), Quaternion.identity)
                        .transform.parent = MainManager.Instance.Environment; 
            _usedPos.Add(pos);
        }
    }
    public void DecorateWalls(List<GameObject> props, HashSet<Vector2Int> walls) {
        if (props.Count <= 0)
            return;
        int propCount = Random.Range(4, 8);
        int activeCount = 0;
        while (propCount > activeCount) {
            //do not spawn props on top of each other
            Vector2Int pos = new();
            do
                pos = Floor.ElementAt(Random.Range(0, Floor.Count));
            while (_usedPos.Contains(pos) || Corridor.Contains(pos));
            //spawn prop
            if(walls.Contains(pos + new Vector2Int(0, 1))) {
                GameObject.Instantiate(props[Random.Range(0, props.Count)], new Vector3(pos.x, pos.y, 0), Quaternion.identity)
                    .transform.parent = MainManager.Instance.Environment;
                activeCount++;
                _usedPos.Add(pos);
            }
        }
    }
}
public enum RoomType {
    Start, Enemy, Treasure, Key, Exit, Boss
}
