using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ANP_Target : MonoBehaviour
{
    protected NP_MainController _mainController;
    [HideInInspector]
    public GameObject Target;
    public virtual void Initialize(NP_MainController mainController) {
        _mainController = mainController;
    }
    public GameObject[] FindEnemies() {
        return GameObject.FindGameObjectsWithTag("Enemy");
    }
    public GameObject[] FindFriends() {
        GameObject[] summons = GameObject.FindGameObjectsWithTag("Summoned");
        GameObject[] all = new GameObject[summons.Length + 1];
        for (int i = 0; i < summons.Length; i++)
            all[i] = summons[i];
        all[all.Length - 1] = _mainController.Player.gameObject;
        return all;
    }
}
