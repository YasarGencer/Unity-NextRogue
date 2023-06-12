using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerList", menuName = "ScriptableObjects/PlayerList", order = 0)]

public class PlayerList : ScriptableObject
{
    [SerializeField]
    List<PlayerElement> _list;
    public List<PlayerElement> GetList() {
        return _list;
    }
    public int GetCount() { return _list.Count; }
    public PlayerElement GetPlayer(int index) {
        return _list[index];
    }
    [System.Serializable]
    public struct  PlayerElement {
        public P_Stats Stat;
        public GameObject Player;
        public GameObject PlayerForUI;
    }
}
