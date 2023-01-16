using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    bool _isPlayer;
    AStats _stats;
    public void Initialize() {

        _isPlayer = gameObject.CompareTag("Player");
        _stats = _isPlayer ? GetComponent<PlayerStats>() : GetComponent<NonPlayerStats>();
    }
    public void GetDamage(float value, Transform source) {

        _stats.Health -= value;
        if (_stats.Health <= 0)
            Die();

        GetComponent<Animator>().SetTrigger("hit");

        //StartCoroutine(Push(source));
        if (!_isPlayer)
            GetComponent<NonPlayerMainController>().ChangeTarget(source.gameObject);
        else
            GetComponent<PlayerMainController>().UI.SetSlider(GetComponent<PlayerMainController>().UI.HealthSlider, GetComponent<PlayerMainController>().Stats.MaxHealth, GetComponent<PlayerMainController>().Stats.Health);
    }
    public void GainHealth(float value) {
        _stats.Health += value;
        _stats.Health = _stats.Health > _stats.MaxHealth ? _stats.MaxHealth : _stats.Health;
    }
    IEnumerator Push(Transform source) {
        Vector2 dir = source.position - transform.position;
        dir = -dir.normalized;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.AddForce(dir * 150 * rb.mass);
        yield return new WaitForSeconds(1f);
        rb.velocity = Vector2.zero;
    }
    void Die() {
        GetComponent<Animator>().SetTrigger("die");
        if (_isPlayer) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else {
            Destroy(gameObject);
        }
    }
}
