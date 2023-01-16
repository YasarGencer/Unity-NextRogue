using System.Collections;
using System.Threading;
using UnityEngine;

public class SpellSummonAidProjectile : APlayerProjectile {
    [SerializeField] GameObject[] summonableObjects;
    [SerializeField] Vector2 summonPos;
    [SerializeField] int summonCount;
    int summoned = 0;
    public override void Initialize(Vector3 mousePos, float damage, float time) {
        base.Initialize(mousePos, damage, time);
        summoned = 0;
        Invoke("Summon", .5f);
    }
    void Summon() {
        StartCoroutine(SummonIEnum());
    }
    IEnumerator SummonIEnum() {
        summoned++;
        Instantiate(
            summonableObjects[Random.Range(0, summonableObjects.Length)],
            transform.position + new Vector3(Random.Range(-summonPos.x, summonPos.x), Random.Range(-summonPos.y, summonPos.y), 0),
            Quaternion.identity);
        yield return new WaitForSeconds(.1f);
        if (summoned <= summonCount)
            StartCoroutine(SummonIEnum());
    }
}