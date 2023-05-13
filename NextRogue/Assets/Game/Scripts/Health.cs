using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour {
    bool _isInit;
    bool _isPlayer;
    AStats _stats; 
    public void Initialize() {
        if (_isInit)
            return;
        _isInit = true;

        _isPlayer = gameObject.CompareTag("Player");
        _stats = _isPlayer ? GetComponent<P_MainController>().Stats : GetComponent<NP_MainController>().Stats; 
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

        _stats.Health -= value;

        MainManager.Instance.CanvasManager.Player_GUI_HUD.DamageText(true, value.ToString(), transform.position);
        Destroy(Instantiate(_stats.HitParticle, transform.position, Quaternion.identity), 5f);

        GetComponent<Animator>().SetTrigger("hit");

        if (_isPlayer)
            MainManager.Instance.CanvasManager.Player_GUI_HUD.SetHealth();
        if (_stats.Health <= 0) 
            Die();
    }
    public void GainHealth(float value) {
        _stats.Health += value;
        _stats.Health = _stats.Health > _stats.MaxHealth ? _stats.MaxHealth : _stats.Health;
        MainManager.Instance.CanvasManager.Player_GUI_HUD.DamageText(false, value.ToString(), transform.position);
        if (_isPlayer)
            MainManager.Instance.CanvasManager.Player_GUI_HUD.SetHealth();
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
