using UnityEngine;

public class Corpse : MonoBehaviour
{
    [SerializeField] GameObject CorpseObject;
    public GameObject GetCorpse() {
        return CorpseObject;
    }
}
