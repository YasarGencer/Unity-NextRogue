using System; 
using System.Threading.Tasks;
using UniRx;
using UnityEngine; 

public class IronMaiden : MonoBehaviour {
    [SerializeField] int damage;
    [SerializeField] GameObject projectile;
    [SerializeField] float stayClosed;
    [SerializeField] float stayOpen;
    bool isOpen = false;
    float activeTimer = 0;
    Animator animator;

    IDisposable updateRX;
    void Start() {
        MainManager.Instance.EventManager.onGamePause += Pause;
        MainManager.Instance.EventManager.onGameUnPause += UnPause;
        animator = GetComponent<Animator>(); 
        UnPause();
    }
    private void UpdateRX(long obj) {
        activeTimer -= Time.deltaTime;
        if (activeTimer < 0) { 
            if (isOpen)
                Close();
            else
                Open();
        } 
    }
    async void Open() {
        Debug.Log("actim");
        isOpen = true;
        activeTimer = stayOpen;
        animator.SetTrigger("Open");
        await Task.Delay(500);
        Debug.Log("ates");
        Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<AProjectile>().Initialize(Vector3.zero, damage, 1f, 150, new DOTInfo(DOTType.BLEED, 0, 0));
    }
    void Close() {
        Debug.Log("kapadim");
        animator.SetTrigger("Close");
        isOpen = false;
        activeTimer = stayClosed;
    }
    private void OnDestroy() {
        MainManager.Instance.EventManager.onGamePause -= Pause;
        MainManager.Instance.EventManager.onGameUnPause -= UnPause;
    }
    private void UnPause() {
        updateRX?.Dispose();
        updateRX = Observable.EveryUpdate().TakeUntilDisable(this).Subscribe(UpdateRX);
        animator.speed = 1f;
    }

    private void Pause() {
        updateRX?.Dispose();
        animator.speed = 0;
    }
}
