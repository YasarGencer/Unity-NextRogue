using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "TV_", menuName = "ScriptableObjects/PDG/TV")]
public class TilemapVisualizerData : ScriptableObject
{
    public TileBase[] FloorTile;


    [Header("Wall")]
    public TileBase
        Top;
    public TileBase Right, Left, Bottom, Full,
        CornerDownLeft, CornerDownRight,
        DownLeft, DownRight, UpLeft, UpRight;

}
