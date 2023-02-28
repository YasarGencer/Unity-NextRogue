using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    bool _isPlayer;
    AStats _stats;
    public void Initialize() {

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
        _stats.Health -= value;

        MainGUIHUD.Instance.DamageText(true, value.ToString(), transform.position);

        GetComponent<Animator>().SetTrigger("hit");

        if (_isPlayer)
            MainGUIHUD.Instance.SetHealth(_stats.Health);
        if (_stats.Health <= 0) 
            Die();
    }
    public void GainHealth(float value) {
        _stats.Health += value;
        _stats.Health = _stats.Health > _stats.MaxHealth ? _stats.MaxHealth : _stats.Health;
        MainGUIHUD.Instance.DamageText(false, value.ToString(), transform.position);
        if (_isPlayer)
            MainGUIHUD.Instance.SetHealth(_stats.Health);
    }
    public void Die() {
        if (transform.CompareTag("Player"))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //GetComponent<Animator>().SetTrigger("die");
        Animator corpse = Instantiate(_stats.Corpse, transform.position, Quaternion.identity).GetComponent<Animator>();
        corpse.SetTrigger("die");

        Destroy(corpse.gameObject, 5f);
        GetComponent<NP_MainController>().Die();
    }
}
