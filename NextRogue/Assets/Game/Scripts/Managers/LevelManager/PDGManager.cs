using System.Threading.Tasks;
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
        _tilemapVisualizer?.Initialize();

        await Task.Run(() => {
            _corridorFirst?.Initialize();
        });
         
        _rooms?.Initialize();  
    }
}
