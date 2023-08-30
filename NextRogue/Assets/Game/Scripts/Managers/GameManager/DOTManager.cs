using System; 
using UniRx;
using UnityEngine;

public class DOTManager : MonoBehaviour
{
    IDisposable _cycleRX;

    [SerializeField,Tooltip("Time inbetween cycles")] float _cycleTime = 5f;
    float _cycleTimer;
    public void Initialize() { 
        MainManager.Instance.EventManager.onGamePause += OnGamePause;
        MainManager.Instance.EventManager.onGameUnPause += OnGameUnPause; 
        _cycleRX?.Dispose();
        _cycleRX = Observable.EveryUpdate().TakeUntilDisable(this).Subscribe(CycleRX);
        ClearAllDOT();
    } 
    public void Register(DOTReciever reciever) {
        MainManager.Instance.EventManager.onDOTCycle += reciever.RecieveDOTDamage;
    }
    public void UnRegister(DOTReciever reciever) {
        MainManager.Instance.EventManager.onDOTCycle -= reciever.RecieveDOTDamage;
    }
    void CycleRX(long obj) {
        if(_cycleTimer <= 0) {

            MainManager.Instance.EventManager.RunOnDOTCycle(); 
            ResetTimer();
        }
        _cycleTimer -= Time.deltaTime;
    }
    void OnGamePause() {
        _cycleRX?.Dispose();
    }
    void OnGameUnPause() {
        _cycleRX?.Dispose();
        _cycleRX = Observable.EveryUpdate().TakeUntilDisable(this).Subscribe(CycleRX);
    }
    void ResetTimer() {
        _cycleTimer = _cycleTime;
    } 
    void ClearAllDOT() {
        foreach (var item in GameObject.FindObjectsOfType<DOTReciever>()) {
            item.ClearDOT();
        }
    }

}
