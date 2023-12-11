using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour {
    bool _isInit;
    bool _isPlayer;
    [SerializeField]
    bool _isScaleChanged;
    AStats _stats;
    DOTReciever _dotReciever;
    

    BossManager _bossManager;
    CameraTarget _cameraTarget;
    Canvas_Player_GUI_HUD _hud { get { return MainManager.Instance.CanvasManager.Player_GUI_HUD; } }
    public DOTReciever DOTReciever { get { return _dotReciever;} }
    public void Initialize() {
        if (_isInit)
            return;
        _isInit = true;

        _isPlayer = gameObject.CompareTag("Player");
        _stats = _isPlayer ? GetComponent<P_MainController>().Stats : GetComponent<ANP_MainController>().Stats;
        _dotReciever = gameObject.AddComponent<DOTReciever>();
        _dotReciever.Initialize(this);

        if (gameObject.CompareTag("Boss"))
            _bossManager = GameObject.FindObjectOfType<BossManager>() as BossManager;
        if (_isPlayer)
            _cameraTarget = GameObject.FindObjectOfType<CameraTarget>();
    }
    public void GetDamage(float value, Transform source) {
        if(GetComponent<Destructable>() != null) {
            GetComponent<Destructable>().Destruct();
            return;
        }

        if (!_stats)
            return;
        if (_stats.IsInvincable)
            return;
        if(_bossManager != null) {
            _bossManager?.UpdateHealthBar();
            _bossManager.ActivePhase.Boss.PhaseController?.HealthPhaser();
        }

        _stats.Health -= value;

        AudioManager.PlaySound(_stats.HitSound, transform);

        _hud.DamageText(true, value.ToString(), transform.position);
        Destroy(Instantiate(_stats.HitParticle, transform.position, Quaternion.identity), 5f);

        //GetComponent<Animator>().SetTrigger("hit");
        transform.DOPunchScale(Vector3.one * .25f, .25f);

        if (_isPlayer) {
            _hud.SetHealth();
            _cameraTarget.Shake();
        }
        if (_isScaleChanged)
        {
            transform.localScale = Vector3.one * (_stats.Health / 20);
        }
        if (_stats.Health <= 0) 
            Die(); 
    }
    public void GainHealth(float value) {
        _stats.Health += value;
        _stats.Health = _stats.Health > _stats.MaxHealth ? _stats.MaxHealth : _stats.Health;
        _hud.DamageText(false, value.ToString(), transform.position);
        if (_isPlayer)
            _hud.SetHealth();
    }
    public void Die() {
        if (transform.CompareTag("Player")) { 
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            return;
        }
        //GetComponent<Animator>().SetTrigger("die");
        Animator corpse = Instantiate(_stats.Corpse, transform.position, Quaternion.identity).GetComponent<Animator>();
        corpse.SetTrigger("die");
        GameObject obj = _stats.EXPOrb;
        if (obj != null) { 
            var drop = Random.Range(0, 100) <= 50;
            if (drop)
                Instantiate(obj, transform.position, Quaternion.identity);
        }
        Destroy(corpse.gameObject, 5f);
        GetComponent<NP_MainController>().Die();
    }
}
