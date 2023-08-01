using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBarrageChallange : MonoBehaviour
{
    private static int killCounterIceBarrage = 0;
    private void Start()
    {
        killCounterIceBarrage = 0;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            float enemyHealth = collision.gameObject.GetComponent<NP_MainController>().Stats.Health;

            if (enemyHealth <= 0)
            {
                killCounterIceBarrage++;
                //Debug.Log(collision.gameObject+" = "+ toplamDüþmanÖldürmeSayaç);
            }
            if (killCounterIceBarrage >= 5)
            {
                MainManager.Instance.GameManager.ChallangeManager.RegisterChallangeDone(SpellType.IceBarrage);
            }
        }
    }
}
