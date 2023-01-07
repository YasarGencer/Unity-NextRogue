using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    bool _isPlayer;
    PlayerStats playerStats;
    EnemyStateHandler.Enemy _enemy;
    public void Initialize() {
        _isPlayer = gameObject.CompareTag("Player");

        if (!_isPlayer)
            _enemy = GetComponent<EnemyStateHandler>().GetEnemy();
        else
            playerStats = gameObject.GetComponent<InputManager>().PlayerStats;
    }
    public void GetDamage(float value, Transform source) {

        if (_isPlayer) {
            playerStats.Health -= value;
            if (playerStats.Health <= 0)
                Die();
        } else {
            _enemy.Health -= value;
            if (_enemy.Health <= 0)
                Die();
            GetComponent<EnemyStateHandler>().SetEnemy(_enemy);
        }
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
