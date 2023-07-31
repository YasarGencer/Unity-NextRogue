using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class DualShotChallenge : MonoBehaviour
{
    private int  destructedObjects;
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Destructable"))
        {
            destructedObjects = PlayerPrefs.GetInt(nameof(destructedObjects));
            destructedObjects++;
            PlayerPrefs.SetInt(nameof(destructedObjects), destructedObjects);
            


            if (PlayerPrefs.GetInt(nameof(destructedObjects)) <= 101)
            {
                MainManager.Instance.ChallangeManager.RegisterChallangeDone(SpellType.DualShot);
            }
        }
    }
}
