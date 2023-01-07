using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AEnemyStates : MonoBehaviour {

    protected EnemyMainController _mainController;
    public virtual void ActivateState(EnemyMainController mainController) {
        _mainController= mainController;
    }
    public virtual void DeactivateState() {
        _mainController = null;
    }
}
