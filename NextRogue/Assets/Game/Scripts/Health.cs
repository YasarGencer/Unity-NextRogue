using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    bool _isPlayer;
    AStats _stats;
    public void Initialize() {
        _isPlayer = gameObject.CompareTag("Player");

        if (!_isPlayer)
            _stats = GetComponent<EnemyMainController>().Stats;
        else
            _stats = GetComponent<PlayerMainController>().Stats;
    }
    public void GetDamage(float value, Transform source) {

        _stats.Health -= value;
        if (_stats.Health <= 0)
            Die();

        StartCoroutine(Push(source));
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
        if (_isPlayer) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else {
            Destroy(gameObject);
        }
    }
}
