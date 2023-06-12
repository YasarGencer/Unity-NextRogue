using System; 
using UniRx;
using UnityEngine;

public abstract class ANP_Spell : ScriptableObject {
    protected NP_MainController _mainController;
    public NP_MainController MainController { get { return _mainController; } set { _mainController = value; } }
 
    [SerializeField]
    bool _isInit = false; 
    public bool IsInit { get { return _isInit; } set { _isInit = value; } } 



    //public bool IsBasic;
    public string Name;
    [TextArea]
    public string Description; 

    public AudioClip Sound;

    public GameObject Spell;
    protected GameObject Instantiated;

    public float Damage;
    public float Speed;
    public float UseRange;

    public float CastingTime;
    float _currentTimeCast;
    IDisposable _castRX;

    public float CooldownTime;
    protected float _currentTimeCooldown;
    IDisposable _cooldownRX;

    [SerializeField] protected bool stopAudio;


    public virtual void Initialize(NP_MainController mainController) { 
        if (_isInit && _currentTimeCooldown < CooldownTime)
            return;
        RegisterEvents();

        _mainController = mainController;
        Instantiated = null;

        _currentTimeCast = CastingTime;
        _currentTimeCooldown = CooldownTime;

        if (CastingTime > 0)
            StartCasting();
        else
            ActivateSpell();
        _isInit = true;
    }
    public abstract bool CheckConditions();
    public virtual void ActivateSpell() { 
        _mainController.Animator.SetTrigger("spell"); 
        if (Sound)
            AudioManager.PlaySound(Sound, _mainController.transform, AudioManager.AudioVolume.sfx, stopAudio);
        StartCooldown();
    }
    public void StartCasting() { _castRX?.Dispose(); _castRX = Observable.EveryUpdate().TakeUntilDisable(_mainController).Subscribe(CastingTimer); }
    public void StopCasting() { _castRX?.Dispose(); ActivateSpell(); _currentTimeCast = CastingTime; }
    public void StartCooldown() { _cooldownRX?.Dispose(); _cooldownRX = Observable.EveryUpdate().TakeUntilDisable(_mainController).Subscribe(CooldownTimer); }
    public void StopCooldown() { _cooldownRX?.Dispose(); _currentTimeCooldown = CooldownTime; }
    public void CastingTimer(long obj) {
        if (_currentTimeCast <= 0) {
            StopCasting();
            return;
        }
        _currentTimeCast -= Time.deltaTime;
    }
    public void CooldownTimer(long obj) {
        if (_currentTimeCooldown <= 0) {
            StopCooldown();
            return;
        }
        _currentTimeCooldown -= Time.deltaTime; 
    }
    public GameObject JustCast(Vector3 pos, bool follow) {
        GameObject projectile;
        if (follow) {
            projectile = Instantiate(
                Spell,
                _mainController.transform
                ); 
        } else { 
            projectile = Instantiate(
                Spell,
                pos,
                Quaternion.identity
                ); 
        }
        if(projectile.GetComponent<AProjectile>() != null) {
            projectile.GetComponent<AProjectile>()
                    .Initialize(_mainController.transform.position,
                    Damage, CooldownTime, Speed);
        }
        return projectile;
    }  
    //EVENTS
    void RegisterEvents() {
        MainManager.Instance.EventManager.onGamePause += OnGamePause;
        MainManager.Instance.EventManager.onGameUnPause += OnGameUnPause;
    }
    void OnGamePause() {
        _cooldownRX?.Dispose();
    }
    void OnGameUnPause() {
        StartCooldown();
    }
}
