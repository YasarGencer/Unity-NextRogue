using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class ImpailingShotsChallenge : MonoBehaviour
{
    private int killCounterImpailingShot = 0;

    private void Start()
    {
        killCounterImpailingShot = 0;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        float enemyHealth = collision.gameObject.GetComponent<NP_MainController>().Stats.Health;
        if (enemyHealth <= 0)
        {
            killCounterImpailingShot ++;
        }
        if (killCounterImpailingShot<=3)
        {
            MainManager.Instance.GameManager.ChallangeManager.RegisterChallangeDone(SpellType.ImpailingShot);
        }
    }
}
