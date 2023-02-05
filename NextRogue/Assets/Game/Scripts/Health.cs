using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEditorInternal;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] GameObject _corpse;
    bool _isPlayer;
    AStats _stats;
    MainGUIHUD _ui;
    public void Initialize() {

        _isPlayer = gameObject.CompareTag("Player");
        _stats = _isPlayer ? GetComponent<P_MainController>().Stats : GetComponent<NonPlayerMainController>().Stats;
        _ui = _isPlayer ? GetComponent<P_MainController>().UI : null;
    }
    public void GetDamage(float value, Transform source) {
        if (!_stats)
            return;
        _stats.Health -= value;

        GetComponent<Animator>().SetTrigger("hit");

        if (_isPlayer)
            _ui.SetSlider(_ui.HealthSlider, _stats.MaxHealth, _stats.Health);
        else
            StartCoroutine(_stats.Clamp());

        if (_stats.Health <= 0) 
            Die();
    }
    public void GainHealth(float value) {
        _stats.Health += value;
        _stats.Health = _stats.Health > _stats.MaxHealth ? _stats.MaxHealth : _stats.Health;
        if(_isPlayer)
            _ui.SetSlider(_ui.HealthSlider, _stats.MaxHealth, _stats.Health);
    }
    public void Die() {
        GetComponent<Animator>().SetTrigger("die");
        Animator corpse = Instantiate(_corpse, transform.position, Quaternion.identity).GetComponent<Animator>();
        corpse.SetTrigger("die");

        if (this.CompareTag("Summoned")) {
            corpse.gameObject.tag = "FriendlyCorpse";
            Destroy(corpse.gameObject, 3f);
        }
        Destroy(gameObject);
    }
}
