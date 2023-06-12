using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "SubjugateWill", menuName = "ScriptableObjects/Spells/SubjugateWill")]
public class _SubjugateWill : ASpell {
    [SerializeField]
    int _subjugateCount = 3;
    [SerializeField]
    GameObject _subhugateHeader;
    public override void Initialize(P_MainController mainController, int value) {
        base.Initialize(mainController, value);
    }
    public override void ActivateSpell() {
        base.ActivateSpell();
        var subjugates = FindClosest(_subjugateCount);
        foreach (var item in subjugates) {
            var subjugate = Instantiate(item.gameObject, item.transform.position, Quaternion.identity);
            
            subjugate.tag = "Summoned";
            subjugate.layer = 8;

            Destroy(Instantiate(Spell, subjugate.transform), 2f);
            Destroy(subjugate.GetComponent<ANP_Target>());

            subjugate.AddComponent<NP_Target_Summoned>();

            Instantiate(_subhugateHeader, subjugate.transform);

            subjugate.GetComponent<NP_MainController>().Initialize(0);

            Destroy(item.gameObject);
        }
    }
    Transform[] FindClosest(int value) {
        var allEnemies = new List<Transform>(); 
        foreach (UnityEngine.Transform item in MainManager.Instance.Enemies)
            allEnemies.Add(item);
        var nClosest = allEnemies.OrderBy(t => (t.position - _mainController.transform.position).sqrMagnitude)
                           .Take(value)   //or use .FirstOrDefault();  if you need just one
                           .ToArray();
        return nClosest;
    }
}
