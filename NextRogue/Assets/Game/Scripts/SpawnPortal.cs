using System; 
using UniRx;
using UnityEngine;

public class SpawnPortal : MonoBehaviour {
    [SerializeField] bool canReverseEnter;
    [SerializeField] float range;
    IDisposable updateRX;
    Vector3 player { get { return MainManager.Instance.Player.transform.position; } }
    private void Start() {
    }
    void UpdateRX(long obj) {
        if(Vector3.Distance(player,this.transform.position) <= range)
            updateRX?.Dispose();
    }
}
