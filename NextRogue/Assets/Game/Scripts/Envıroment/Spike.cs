using UnityEngine; 

public class Spike : MonoBehaviour
{ 
    [SerializeField] int damage;
    Animator animator;
    void Start() {
        MainManager.Instance.EventManager.onGamePause += Pause;
        MainManager.Instance.EventManager.onGameUnPause += UnPause;
        animator = GetComponent<Animator>();
        GetComponent<Damager>().Initialize(damage); 
        UnPause();
    }
    private void UnPause() { 
        animator.speed= .5f;
    }

    private void Pause() {
        animator.speed = 0;
    } 
}
