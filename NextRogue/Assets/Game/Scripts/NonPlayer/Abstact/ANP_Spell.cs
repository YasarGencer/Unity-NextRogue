using System; 
using UniRx;
using UnityEngine;

public abstract class ANP_Spell : ScriptableObject {
    protected ANP_MainController _mainController;
    public ANP_MainController MainController { get { return _mainController; } set { _mainController = value; } }
 
    [SerializeField]
    protected bool _isInit = false; 
    public bool IsInit { get { return _isInit; } set { _isInit = value; } }

    [SerializeField]
    float _spellTimer = 1f;
    [SerializeField]
    float _moveTimer = 1f;
    [SerializeField]
    float _attackTimer = 1f;

    public float SpellTimer { get { return _spellTimer; } }
    public float MoveTimer { get { return _moveTimer; } }
    public float AttackTimer { get { return _attackTimer; } } 

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

    public DOTInfo DOTInfo;

    public float CastingTime;
    float _currentTimeCast;
    IDisposable _castRX;

    public float CooldownTime;
    protected float _currentTimeCooldown;
    IDisposable _cooldownRX;

    [SerializeField] protected bool stopAudio;


    public virtual void Initialize(ANP_MainController mainController) {  
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
    public virtual bool CheckConditions(ANP_MainController mainController) { 
        _mainController = _mainController == null ? mainController : _mainController;
        if (_mainController == null)
            return false; 
        if (_mainController.Target.Target == null)
            return false; 
        if (_isInit == false)
            return true; 
        if (_currentTimeCooldown < CooldownTime)
            return false; 
        return true;
    }
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
    public GameObject JustCast(Vector3 pos, bool follow = false) {
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
        if(projectile.GetComponent<ANP_Projectile>() != null) {
            projectile.GetComponent<ANP_Projectile>()
                    .Initialize(pos,
                    Damage, CooldownTime, Speed, DOTInfo);
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
