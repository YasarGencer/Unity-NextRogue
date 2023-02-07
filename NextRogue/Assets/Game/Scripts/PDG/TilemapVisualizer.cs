using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapVisualizer : MonoBehaviour
{
    [SerializeField]
    private Tilemap _floorTilemap, _wallTilemap;
    [SerializeField]
    private TilemapVisualizerData _TVData;
    [SerializeField]
    private TileBase _def;

    public void Initialize() {
        Clear();
    }
    public void Clear() {
        _floorTilemap.ClearAllTiles();
        _wallTilemap.ClearAllTiles();
    }
    public void PaintFloorTiles(IEnumerable<Vector2Int> floorPoses) {
        PaintTiles(floorPoses, _floorTilemap, _TVData.FloorTile);
    }
    public void PaintDefaulToFloor(IEnumerable<Vector2Int> floorPoses) {
        PaintTiles(floorPoses, _floorTilemap, _def);
    }

    public void PaintTiles(IEnumerable<Vector2Int> floorPoses, Tilemap tilemap, TileBase tile) {
        foreach (var position in floorPoses) {
            PaintSingleTile(tilemap, tile, position);
        }
    }
    public void PaintTiles(IEnumerable<Vector2Int> floorPoses, Tilemap tilemap, TileBase[] tile) {
        foreach (var position in floorPoses) {
            PaintSingleTile(tilemap, tile[new System.Random().Next(0,tile.Length)], position);
        }
    }

    public void PaintSingleTile(Tilemap tilemap, TileBase tile, Vector2Int position) {
        var tilePosition = tilemap.WorldToCell((Vector3Int)position);
        tilemap.SetTile(tilePosition, tile);
    }
    public void PaintSingleTile(Vector2Int position) {
        var tilePosition = _floorTilemap.WorldToCell((Vector3Int)position);
        _floorTilemap.SetTile(tilePosition, _def);
    }

    internal void PaintSingleBasicWall(Vector2Int pos, string binaryType) {
        int typeAsInt = Convert.ToInt32(binaryType, 2);
        TileBase tile = null;
        if (WallTypesHelper.wallTop.Contains(typeAsInt))
            tile = _TVData.Top;
        else if (WallTypesHelper.wallSideLeft.Contains(typeAsInt))
            tile = _TVData.Left;
        else if (WallTypesHelper.wallSideRight.Contains(typeAsInt))
            tile = _TVData.Right;
        else if (WallTypesHelper.wallBottm.Contains(typeAsInt))
            tile = _TVData.Bottom;
        else if (WallTypesHelper.wallFull.Contains(typeAsInt))
            tile = _TVData.Full;
        if (tile != null)
            PaintSingleTile(_wallTilemap, tile, pos);
    }

    internal void PaintSingleCornerWall(Vector2Int pos, string binaryType) {
        int typeAsInt = Convert.ToInt32(binaryType, 2);
        TileBase tile = null;

        if (WallTypesHelper.wallInnerCornerDownLeft.Contains(typeAsInt))
            tile = _TVData.CornerDownLeft;
        else if (WallTypesHelper.wallInnerCornerDownRight.Contains(typeAsInt))
            tile = _TVData.CornerDownRight;
        else if (WallTypesHelper.wallDiagonalCornerDownLeft.Contains(typeAsInt))
            tile = _TVData.DownLeft;
        else if (WallTypesHelper.wallDiagonalCornerDownRight.Contains(typeAsInt))
            tile = _TVData.DownRight;
        else if (WallTypesHelper.wallDiagonalCornerUpLeft.Contains(typeAsInt))
            tile = _TVData.UpLeft;
        else if (WallTypesHelper.wallDiagonalCornerUpRight.Contains(typeAsInt))
            tile = _TVData.UpRight;
        else if (WallTypesHelper.wallFullEightDirections.Contains(typeAsInt))
            tile = _TVData.Full;
        else if (WallTypesHelper.wallBottmEightDirections.Contains(typeAsInt))
            tile = _TVData.Bottom;

        if (tile != null)
            PaintSingleTile(_wallTilemap, tile, pos);
    }
}
