using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class EnemyStateHandler : MonoBehaviour {
    EnemyMainController _mainController;
    [HideInInspector]
    public EnemyStateTypes _activeState = EnemyStateTypes.NULL;
    public void Initialize(EnemyMainController mainController) {
        _mainController= mainController;
        SelectState(EnemyStateTypes.ATTACK);
    }
    public void SelectState(EnemyStateTypes state) {
        if(_activeState != EnemyStateTypes.NULL)
            Destroy(GetComponent<AEnemyStates>());
        switch (state) {
            case EnemyStateTypes.ATTACK:
                gameObject.AddComponent<EnemyStateAttack>();
                break;
            default:
                break;
        }
        _activeState = state;
        gameObject.GetComponent<AEnemyStates>().ActivateState(_mainController);
    }

    public enum EnemyStateTypes {
        NULL,
        ATTACK,
    }
}
