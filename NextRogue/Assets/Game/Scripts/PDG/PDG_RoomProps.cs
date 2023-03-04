using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RoomProps", menuName = "ScriptableObjects/PDG/RoomProps")]
public class PDG_RoomProps : ScriptableObject {

    public TilemapVisualizerData Data;
    [Space(15f)]
    [Header("Room Props")] 
    public List<GameObject> StartRoom;
    public List<GameObject> EnemyRoom;
    public List<GameObject> ExitRoom;
    [Space(15f)]
    [Header("Obstacles")]
    public List<GameObject> CenterProps;
    public List<GameObject> WallProps;
}
