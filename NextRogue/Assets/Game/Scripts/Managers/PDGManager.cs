using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class PDGManager : MonoBehaviour
{
    [SerializeField]
    private TilemapVisualizer _tilemapVisualizer;
    [SerializeField]
    private PDG_SimpleRandomWalk _simpleRandomWalk;
    [SerializeField]
    private PDG_CorridorFirst _corridorFirst;
    [SerializeField]
    private PDG_Rooms _rooms;

    public TilemapVisualizer TilemapVisualizer { get { return _tilemapVisualizer; } }
    public PDG_SimpleRandomWalk SimpleRandomWalk { get { return _simpleRandomWalk; } }
    public PDG_CorridorFirst CorridorFirst { get { return _corridorFirst; } }
    public PDG_Rooms Rooms { get { return _rooms; } }
    public async void Initialize() {
        if (_tilemapVisualizer)
            _tilemapVisualizer.Initialize();

        if (_simpleRandomWalk)
            _simpleRandomWalk.Initialize();

        if (_corridorFirst) {
            await Task.Run(() => {
                _corridorFirst.Initialize();
            });
        }
        if (_rooms)
            _rooms.Initialize();
    }
}
