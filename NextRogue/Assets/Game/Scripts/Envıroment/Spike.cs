using UnityEngine; 

public class Spike : MonoBehaviour
{ 
    [SerializeField] int damage;
    [SerializeField] DOTInfo dotInfo;
    Animator animator;
    void Start() {
        MainManager.Instance.EventManager.onGamePause += Pause;
        MainManager.Instance.EventManager.onGameUnPause += UnPause;
        animator = GetComponent<Animator>();
        GetComponent<Damager>().Initialize(damage, dotInfo); 
        UnPause();
    }
    private void OnDestroy() {
        MainManager.Instance.EventManager.onGamePause -= Pause;
        MainManager.Instance.EventManager.onGameUnPause -= UnPause;
    }
    private void UnPause() { 
        animator.speed= .5f;
    }

    private void Pause() {
        animator.speed = 0;
    } 
}
