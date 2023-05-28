using Unity.VisualScripting;
using UnityEngine;

public class Corpse : MonoBehaviour
{
    
    [SerializeField] GameObject CorpseObject;
    public GameObject GetCorpse() {
        return CorpseObject;
    }
    private void Start() {
        GetComponent<Animator>().SetTrigger("die");
    }
}
