using UnityEngine;

public class Destructable : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _drops;
    [SerializeField]
    private int _dropsCount = 0;
    public void Destruct() {
        GetComponent<Animator>().SetTrigger("Destroy");
        Destroy(GetComponent<Rigidbody2D>());
        Destroy(GetComponent<CircleCollider2D>());
        for (int i = 0; i < _dropsCount; i++) {
            Instantiate(_drops[UnityEngine.Random.Range(0, _drops.Length)], transform.position, Quaternion.identity);
        }
    }
}
