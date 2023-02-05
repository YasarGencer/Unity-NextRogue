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
    private TileBase _floorTile, _wallTile;
    public TileBase FloorTile { get { return _floorTile; } }
    public TileBase WallTile { get { return _wallTile; } }

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
}
