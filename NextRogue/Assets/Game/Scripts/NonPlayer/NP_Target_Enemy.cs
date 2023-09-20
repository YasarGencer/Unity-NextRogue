using System.Collections;
using UnityEngine;

public class NP_Target_Enemy : ANP_Target
{
    public override void Initialize(ANP_MainController mainController) {
        base.Initialize(mainController);
        StartCoroutine(ChangeTargetRec());
    }
    void ChangeTarget() {
        float dist = 1000;
        foreach (var item in FindFriends()) {
            if (item.gameObject.activeSelf == false)
                continue;
            if(dist > _mainController.Distance(item.transform)) {
                dist = _mainController.Distance(item.transform);
                Target = item;
            }
        }
    }
    IEnumerator ChangeTargetRec() {
        ChangeTarget();
        yield return new WaitForSeconds(.25f);
        StartCoroutine(ChangeTargetRec());
    }
}
