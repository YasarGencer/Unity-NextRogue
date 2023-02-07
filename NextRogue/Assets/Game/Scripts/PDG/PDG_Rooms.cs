using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.VersionControl;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using static UnityEditor.Progress;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class PDG_Rooms : MonoBehaviour
{
    private List<HashSet<Vector2Int>> _floorPoses = new();
    private List<IEnumerable<Vector2Int>> _corridorPoses = new();
    private HashSet<Vector2Int> _allCorridors = new ();
    [SerializeField]
    private List<Room> _rooms = new();

    [Space(15f)]
    [Header("Props")]
    [SerializeField]
    private List<GameObject> _startRoom;
    [SerializeField]
    private List<GameObject> _enemyRoom;
    [SerializeField]
    private List<GameObject> _exitRoom;

    public void Initialize() {
        CreateRooms();
        SortRooms();
        Invoke("DecorateRooms", 3f);
    }
    public void SaveRoom(HashSet<Vector2Int> item) { _floorPoses.Add(item); }
    public void SaveCorridor(IEnumerable<Vector2Int> item) { _corridorPoses.Add(item); }
    public void CreateRooms() {
        HashSet<Vector2Int> allTiles = new HashSet<Vector2Int>();

        foreach (var item in _corridorPoses) {
            allTiles.UnionWith(item);
            _allCorridors.UnionWith(item);
        }
        foreach (var item in _floorPoses)
            allTiles.UnionWith(item);

        MainManager.Instance.PDGManager.TilemapVisualizer.PaintFloorTiles(allTiles);
        WallGenerator.CreateWalls(allTiles, MainManager.Instance.PDGManager.TilemapVisualizer);
    }
    public void DecorateRooms() {
        foreach (var item in _rooms) {
            switch (item.RoomType) {
                case RoomType.Start:
                    break;
                case RoomType.Enemy:
                    item.DecorateRooms(_enemyRoom);
                    break;
                case RoomType.Treasure:
                    break;
                case RoomType.Key:
                    break;
                case RoomType.Exit:
                    break;
                case RoomType.Boss:
                    break;
                default:
                    break;
            }
        }
    }
    public void SortRooms() {
        foreach (var item in _floorPoses) {

            Room room = new();

            Vector2Int firstFloor = new Vector2Int(5, 5);
            foreach (var poses in item) {
                firstFloor = poses;
                break;
            }

            room.Floor = item;
            room.Center = firstFloor;
            room.Distance = Vector2.Distance(Vector2.zero, room.Center);
            room.RoomType = RoomType.Enemy;

            _rooms.Add(room);

        }
        _rooms = _rooms.OrderBy(x => x.Distance).ToList();
        for (int i = 0; i < _rooms.Count; i++) {
            _rooms[i].ID = i;
        }

        _rooms[0].RoomType = RoomType.Start;
        _rooms[_rooms.Count - 1].RoomType = RoomType.Exit;
        _rooms[Random.Range(1, _rooms.Count - 1)].RoomType = RoomType.Treasure;
    }
}
[System.Serializable]
public class Room{
    public int ID;
    public HashSet<Vector2Int> Floor;
    public Vector2Int Center;
    public RoomType RoomType;
    public float Distance;

    public void DecorateRooms(List<GameObject> props) {
        switch (RoomType) {
            case RoomType.Start:
                break;
            case RoomType.Enemy:
                Vector2 pos = Vector3.zero;
                int enemyCount = Random.Range(5, 10);
                for (int i = 0; i < enemyCount; i++) {

                    NonPlayerMainController mainController = GameObject.Instantiate(
                        props[Random.Range(0, props.Count)],
                        MainManager.Instance.Enemies
                        ).GetComponent<NonPlayerMainController>();

                    mainController.transform.position = new Vector3(Center.x, Center.y, 0);
                    mainController.Initialize(this);
                }
                break;
            case RoomType.Treasure:
                break;
            case RoomType.Key:
                break;
            case RoomType.Exit:
                break;
            case RoomType.Boss:
                break;
            default:
                break;
        }
    }
}
public enum RoomType {
    Start, Enemy, Treasure, Key, Exit, Boss
}
