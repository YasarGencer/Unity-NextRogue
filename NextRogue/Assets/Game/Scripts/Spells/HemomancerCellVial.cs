using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "HemomancerCellVial", menuName = "ScriptableObjects/Spells/HemomancerCellVial")]

public class HemomancerCellVial : ASpell{
    [Header("CellVial")]
    public int MaxCell = 5; 
    public float BloodRange;
    public GameObject Particle;
    public override void Initialize(P_MainController mainController, int value) {
        base.Initialize(mainController, value);
    }
    public override void ActivateSpell() {
        base.ActivateSpell();

        _mainController.Stats.SecondaryValue += CheckBloods();
        if(_mainController.Stats.SecondaryValue > MaxCell)
            _mainController.Stats.SecondaryValue = MaxCell; 
        InGameManager.Instance.CanvasManager.Player_GUI_HUD.SetSecondary(_mainController.Stats.SecondaryValue, MaxCell);
    }
    int CheckBloods() {
        var bloods = GameObject.FindGameObjectsWithTag("Blood");
        var count = 0;
        foreach (var item in bloods) {
            var dist = Vector2.Distance(item.transform.position, _mainController.transform.position);
            if (dist <= BloodRange) {
                count++;
                Destroy(Instantiate(Particle, item.transform.position, Quaternion.identity), .5f);
                GameObject.Destroy(item);
            }
        }
        return count;
    }
}
