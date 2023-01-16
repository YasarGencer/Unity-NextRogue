using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class NonPlayerStateHandler : MonoBehaviour {

    NonPlayerMainController _mainController;
    [SerializeField] AStates _movementState;
    [SerializeField] AStates _attackState;


    public void Initialize(NonPlayerMainController mainController) {
        _mainController = mainController;
        Instantiate(_movementState).ActivateState(_mainController);
        Instantiate(_attackState).ActivateState(_mainController);
    }
    /*
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
    */
}