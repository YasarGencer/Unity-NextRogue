using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class DOTReciever : MonoBehaviour
{
    List<DOTInfo> _dotList;
    Health _health;
    public void Initialize(Health health) {
        MainManager.Instance.GameManager.DOTManager.Register(this);
        _health = health;
        _dotList= new List<DOTInfo>();
    }
    public void RecieveDOT(DOTInfo dotInfo) {
        DOTInfo info = new DOTInfo(dotInfo.DOTType, dotInfo.CycleDamage, dotInfo.CycleTime);
        _dotList.Add(info);
    }

    public void RecieveDOTDamage() {
        if (_health == null || _dotList.Count <= 0)
            return;
        Debug.Log("CYCLE");
        for (int i = 0; i < _dotList.Count; i++) {
            DOTInfo item = _dotList[i];
            Debug.Log(i+ ". eleman tipi => " + item.DOTType.ToString() + "   kalan süresi =>" + item.CycleTime.ToString());
            _health.GetDamage(item.CycleDamage, null);
            item.CycleTime -= 1;
            if (item.CycleTime <= 0)
                _dotList.Remove(item);
        } 
    }
    public void ClearDOT() {
        _dotList.Clear();
    }
    public void ClearByType(DOTType type) {
        foreach (var item in _dotList) {
            if(item.DOTType== type) {
                _dotList.Remove(item);
            }
        }
    }
    private void OnDestroy() {
        MainManager.Instance.GameManager.DOTManager.UnRegister(this);
    }
}
