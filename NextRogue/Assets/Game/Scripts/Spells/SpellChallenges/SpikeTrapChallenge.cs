using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrapChallenge : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            float playerHealth = collision.gameObject.GetComponent<P_MainController>().Stats.Health;
            if (playerHealth <= 0)
            {
                MainManager.Instance.ChallangeManager.RegisterChallangeDone(SpellType.SpikeTrap);
            }
        }
    }
}
