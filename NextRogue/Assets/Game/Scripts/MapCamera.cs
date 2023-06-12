using System.Collections.Generic;
using UnityEngine;

public class MapCamera : MonoBehaviour
{
    public Camera MapCam { get; private set; }
    [SerializeField] Transform _follow;
    [SerializeField] float _range;
    IEnumerable<Vector2Int> _allTiles;
    GameObject[] _minimapObjects;
    private void Start() {
        MapCam = GetComponent<Camera>();
        MainManager.Instance.EventManager.onGamePause += UpdateMap;

    }
    private void Update() {
        if (_minimapObjects == null)
            return;
        foreach (var item in _minimapObjects) {
            if (item == null)
                return;
            if (Vector2.Distance(_follow.position, item.transform.position) <= _range) {
                Color color = item.GetComponent<SpriteRenderer>().color;
                color.a = 1;
                item.GetComponent<SpriteRenderer>().color = color;
            }
        }
        if (_allTiles == null)
            return;
        foreach (var tile in _allTiles) {
            if (Vector2.Distance(_follow.position, tile) <= _range)
                MainManager.Instance.LevelManager.PDGManager.TilemapVisualizer.PaintMinimapPart(tile);
        }
    }
    void UpdateMap() {
        transform.position = _follow.position;
        transform.position += new Vector3(0, 0, -10);  
    }
    public void GetTiles(IEnumerable<Vector2Int> allTiles) {
        _allTiles = allTiles;  
    }
    public void GetGameObjects() {
        _minimapObjects = GameObject.FindGameObjectsWithTag("Minimap");
        foreach (var item in _minimapObjects) { 
            Color color = item.GetComponent<SpriteRenderer>().color;
            color.a = 0;
            item.GetComponent<SpriteRenderer>().color = color;
        }
    }
  
}
