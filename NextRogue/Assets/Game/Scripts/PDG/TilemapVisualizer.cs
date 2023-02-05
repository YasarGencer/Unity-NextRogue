using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapVisualizer : MonoBehaviour
{
    [SerializeField]
    private Tilemap _floorTilemap, _wallTilemap;
    public Tilemap FloorTilemap { get { return _floorTilemap; } }
    public Tilemap WallTilemap { get { return _wallTilemap; } }

    [SerializeField]
    private TileBase _floorTile,
        _wallTile_Top, _wallTile_SideRight, _wallTile_SideLeft, _wallTile_Bottom, _wallTile_Full,
        _wallInnerCornerTile_DownLeft, _wallInnerCornerTile_DownRight,
        _wallDiagonalCornerTile_DownLeft, _wallDiagonalCornerTile_DownRight, _wallDiagonalCornerTile_UpLeft, _wallDiagonalCornerTile_UpRight;

    public void PaintFloorTiles(IEnumerable<Vector2Int> floorPoses) {
        PaintTiles(floorPoses, _floorTilemap, _floorTile);
    }

    public void PaintTiles(IEnumerable<Vector2Int> floorPoses, Tilemap tilemap, TileBase tile) {
        foreach (var position in floorPoses) {
            PaintSingleTile(tilemap, tile, position);
        }
    }

    public void PaintSingleTile(Tilemap tilemap, TileBase tile, Vector2Int position) {
        var tilePosition = tilemap.WorldToCell((Vector3Int)position);
        tilemap.SetTile(tilePosition, tile);
    }
    public void Clear() {
        _floorTilemap.ClearAllTiles();
        _wallTilemap.ClearAllTiles();
    }

    internal void PaintSingleBasicWall(Vector2Int pos, string binaryType) {
        int typeAsInt = Convert.ToInt32(binaryType, 2);
        TileBase tile = null;
        if (WallTypesHelper.wallTop.Contains(typeAsInt))
            tile = _wallTile_Top;
        else if (WallTypesHelper.wallSideLeft.Contains(typeAsInt))
            tile = _wallTile_SideLeft;
        else if (WallTypesHelper.wallSideRight.Contains(typeAsInt))
            tile = _wallTile_SideRight;
        else if (WallTypesHelper.wallBottm.Contains(typeAsInt))
            tile = _wallTile_Bottom;
        else if (WallTypesHelper.wallFull.Contains(typeAsInt))
            tile = _wallTile_Full;
        if (tile != null)
            PaintSingleTile(_wallTilemap, tile, pos);
    }

    internal void PaintSingleCornerWall(Vector2Int pos, string binaryType) {
        int typeAsInt = Convert.ToInt32(binaryType, 2);
        TileBase tile = null;

        if (WallTypesHelper.wallInnerCornerDownLeft.Contains(typeAsInt))
            tile = _wallInnerCornerTile_DownLeft;
        else if (WallTypesHelper.wallInnerCornerDownRight.Contains(typeAsInt))
            tile = _wallInnerCornerTile_DownRight;
        else if (WallTypesHelper.wallDiagonalCornerDownLeft.Contains(typeAsInt))
            tile = _wallDiagonalCornerTile_DownLeft;
        else if (WallTypesHelper.wallDiagonalCornerDownRight.Contains(typeAsInt))
            tile = _wallDiagonalCornerTile_DownRight;
        else if (WallTypesHelper.wallDiagonalCornerUpLeft.Contains(typeAsInt))
            tile = _wallDiagonalCornerTile_UpLeft;
        else if (WallTypesHelper.wallDiagonalCornerUpRight.Contains(typeAsInt))
            tile = _wallDiagonalCornerTile_UpRight;
        else if (WallTypesHelper.wallFullEightDirections.Contains(typeAsInt))
            tile = _wallTile_Full;
        else if (WallTypesHelper.wallBottmEightDirections.Contains(typeAsInt))
            tile = _wallTile_Bottom;

        if (tile != null)
            PaintSingleTile(_wallTilemap, tile, pos);
    }
}
