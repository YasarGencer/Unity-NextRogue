using System.Collections.Generic;
using System.Linq; 
using UnityEngine;

public class PDG_Rooms : MonoBehaviour
{
    private List<HashSet<Vector2Int>> _floorPoses;
    private List<IEnumerable<Vector2Int>> _corridorPoses;
    private HashSet<Vector2Int> _allCorridors;
    private HashSet<Vector2Int> _allWalls;
    [SerializeField]
    private List<Room> _rooms;

    [SerializeField]
    private PDG_RoomProps _roomProps;

    public void Initialize() {
        MainManager.Instance.PDGManager.TilemapVisualizer.SetTVData(_roomProps.Data); 
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
        _allCorridors = new();
        _allWalls = new();
        _rooms = new();
    }
    public void CreateRooms() {
        HashSet<Vector2Int> allTiles = new HashSet<Vector2Int>();

        foreach (var item in _corridorPoses) {
            allTiles.UnionWith(item);
            _allCorridors.UnionWith(item);
        }
        foreach (var item in _floorPoses)
            allTiles.UnionWith(item);

        MainManager.Instance.PDGManager.TilemapVisualizer.PaintFloorTiles(allTiles);
        _allWalls = WallGenerator.CreateWalls(allTiles, MainManager.Instance.PDGManager.TilemapVisualizer);

        for (int i = 0; i < _floorPoses.Count; i++)
            _rooms.Add(new(_floorPoses[i], _corridorPoses[i]));
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
        foreach (var item in _rooms) {
            switch (item.RoomType) {
                case RoomType.Start:
                    item.DecorateWalls(_roomProps.WallProps, _allWalls);
                    break;
                case RoomType.Enemy:
                    item.DecorateCenter(_roomProps.CenterProps, _allWalls);
                    item.DecorateWalls(_roomProps.WallProps, _allWalls);
                    item.DecorateEnemyRoom(_roomProps.EnemyRoom);
                    break;
                case RoomType.Treasure:
                    item.DecorateWalls(_roomProps.WallProps, _allWalls);
                    break;
                case RoomType.Key:
                    break;
                case RoomType.Exit:
                    item.DecorateWalls(_roomProps.WallProps, _allWalls);
                    item.DecorateExitRoom(_roomProps.ExitRoom);
                    break;
                case RoomType.Boss:
                    break;
                default:
                    break;
            }
        }
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
        int enemyCount = Random.Range(5, 10);
        for (int i = 0; i < enemyCount; i++) {

            NP_MainController mainController = GameObject.Instantiate(
                props[Random.Range(0, props.Count)],
                MainManager.Instance.Enemies
                ).GetComponent<NP_MainController>();

            mainController.transform.position = new Vector3(Center.x, Center.y, 0);
            mainController.Initialize(this);
        }
    }
    public void DecorateExitRoom(List<GameObject> props) {
        foreach (var item in props) {
            GameObject.Instantiate(
                props[Random.Range(0, props.Count)],
                MainManager.Instance.Enviroment
                ).transform.position = new(Center.x, Center.y, 0);
        }

    }
    public void DecorateCenter(List<GameObject> props, HashSet<Vector2Int> walls) {
        int propCount = Random.Range(3,7);
        int activeCount = 0;
        while (propCount > activeCount) {
            bool decorate = true;

            //do not spawn props on top of each other
            Vector2Int pos = new(0, 0);
            do
                pos = Floor.ElementAt(Random.Range(0, Floor.Count));
            while (_usedPos.Contains(pos) && Corridor.Contains(pos));

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
                    if (Floor.Contains(pos + item * 3))
                        decorate = false;
                    else {
                        decorate = true;
                        break;
                    }
            }
            //spawn prop
            if (decorate) {
                GameObject.Instantiate(props[Random.Range(0, props.Count)], new Vector3(pos.x, pos.y, 0), Quaternion.identity)
                    .transform.parent = MainManager.Instance.Enviroment;
                activeCount++;
                _usedPos.Add(pos);
            }
        }
    }
    public void DecorateWalls(List<GameObject> props, HashSet<Vector2Int> walls) {
        int propCount = Random.Range(4, 8);
        int activeCount = 0;
        while (propCount > activeCount) {
            //do not spawn props on top of each other
            Vector2Int pos = new();
            do
                pos = Floor.ElementAt(Random.Range(0, Floor.Count));
            while (_usedPos.Contains(pos) && Corridor.Contains(pos));
            //spawn prop
            if(walls.Contains(pos + new Vector2Int(0, 1))) {
                GameObject.Instantiate(props[Random.Range(0, props.Count)], new Vector3(pos.x, pos.y, 0), Quaternion.identity)
                    .transform.parent = MainManager.Instance.Enviroment;
                activeCount++;
                _usedPos.Add(pos);
            }
        }
    }
}
public enum RoomType {
    Start, Enemy, Treasure, Key, Exit, Boss
}
