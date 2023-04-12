using System;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class GameManager : MonoBehaviour {

    [SerializeField] AllSpells _spellList;
    [SerializeField] PlayerList _playerList;
    public AllSpells AllSpells { get { return _spellList; }}
    public PlayerList PlayerList { get { return _playerList; }} 
    public void Initialize() { 

        _spellList.Initialize();

        LoadScene(2);
    }
    public void LoadScene(int index, int unload = 0) {
        if(unload != 0)
            UnloadScene(unload);
        SceneManager.LoadScene(index, LoadSceneMode.Additive); 
    }
    public void UnloadScene(int index) {
        MainManager.Instance.EventManager.RunOnLoad();
        SceneManager.UnloadSceneAsync(index);
    } 
}
