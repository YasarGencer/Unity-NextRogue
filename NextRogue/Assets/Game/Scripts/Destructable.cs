using UnityEngine;

public class Destructable : MonoBehaviour
{
    [SerializeField]
    private AudioClip _clip;
    [SerializeField]
    private GameObject[] _drops; 
    [SerializeField, Range(0.1f,1)]
    private float _dropRate = 0;
    public void Destruct() {
        AudioManager.PlaySound(_clip, transform,AudioManager.AudioVolume.environment);

        GetComponent<Animator>().SetTrigger("Destroy");
        Destroy(GetComponent<Rigidbody2D>());
        Destroy(GetComponent<CircleCollider2D>());
        var drop = Random.Range(0, 100) <= _dropRate * 100; 
        if(drop) 
            Instantiate(_drops[UnityEngine.Random.Range(0, _drops.Length)], transform.position, Quaternion.identity); 
    }
}
