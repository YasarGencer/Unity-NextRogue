using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "TV_", menuName = "PDG/TV")]
public class TilemapVisualizerData : ScriptableObject
{
    public TileBase[] FloorTile;
    

    [Header("Wall")]
    public TileBase
        Top, Right, Left, Bottom, Full,
        CornerDownLeft, CornerDownRight,
        DownLeft, DownRight, UpLeft, UpRight;

}
