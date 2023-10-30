using DG.Tweening;
using System; 
using System.Threading.Tasks;
using UniRx;
using UnityEngine; 

public class IronMaiden : MonoBehaviour {
    [SerializeField] int damage;
    [SerializeField] GameObject projectile;
    [SerializeField] float stayClosed;
    [SerializeField] float stayOpen;
    [SerializeField] GameObject opened, closed;
    bool isOpen = false;
    float activeTimer = 0;
    Animator animator;

    IDisposable updateRX;
    void Start() {
        MainManager.Instance.EventManager.onGamePause += Pause;
        MainManager.Instance.EventManager.onGameUnPause += UnPause;
        animator = GetComponent<Animator>(); 
        UnPause();
        opened.SetActive(false);
        closed.SetActive(true);
    }
    private void UpdateRX(long obj) {
        activeTimer -= Time.deltaTime;
        if (activeTimer < 0) {
            transform.DOPunchScale(Vector3.one * .5f, .4f); 
            if (isOpen)
                Close();
            else
                Open();
        } 
    }
    async void Open() {
        opened.SetActive(true);
        closed.SetActive(false); 
        isOpen = true;
        activeTimer = stayOpen;
        animator.SetTrigger("Open");
        //await Task.Delay(500); 
        Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<AProjectile>().Initialize(Vector3.zero, damage, 1.5f, 150, new DOTInfo(DOTType.BLEED, 0, 0));

    }
    void Close() {
        opened.SetActive(false);
        closed.SetActive(true);
        activeTimer = stayClosed;
        isOpen = false;
        animator.SetTrigger("Close");
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
