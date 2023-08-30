using UnityEngine;

public class NecromancerGraveKeeperProjectile : AP_Projectile {
    [SerializeField] float _range;
    public override void Initialize(Vector3 mousePos, float damage, float time, float speed, DOTInfo dotInfo) {
        base.Initialize(mousePos, damage, time, speed, dotInfo);
        transform.localScale = new Vector3(_range, _range, _range);
        Summon();
    }
    void Summon() {
        foreach (var item in GameObject.FindGameObjectsWithTag("Corpse")) {
            if (Vector2.Distance(item.transform.position, transform.position) < _range) {
                Instantiate(item.GetComponent<Corpse>().GetCorpse(), item.transform.position, Quaternion.identity)
                    .GetComponent<NP_MainController>().Initialize(.5f);
                Destroy(item.gameObject);
            }
        }
    }
}