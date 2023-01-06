using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private bool isInit = false;
    [Header("MOVE")]
    [SerializeField] private float _speed;
    [Header("DASH")]
    [SerializeField] private float _dashForce;
    [SerializeField] private float _dashTime;
    [SerializeField] private float _dashCooldown;
    [SerializeField] private int _maxDashes;
    private int _availableDashes;
    
    
    Rigidbody2D rb;
    Animator animator;
    private Vector2 _direction;

    public void Initialize() {

        rb = rb == null? GetComponent<Rigidbody2D>() : rb;
        animator = animator == null ? GetComponent<Animator>() : animator;
        
        _availableDashes = _maxDashes;

        isInit = true;
    }
    private void Update() {
        if (!isInit)
            return;
        transform.Translate(_direction * _speed * Time.deltaTime);
    }
    public void Direction(Vector2 direction) {
        _direction = direction;
        animator.SetFloat("moveDir", _direction.magnitude);
        int x = _direction.x > 0 ? 1 : -1;
        this.transform.localScale = new Vector2(x, 1);
    }
    public void Dash() {
        if (_availableDashes <= 0)
            return;

        _availableDashes--;

        rb.velocity = _direction * _dashForce;

        Invoke("StopDash", _dashTime);
        StartCoroutine(DashEffect());
        StartCoroutine(DashTimer());
    }
    public void StopDash() => rb.velocity = Vector2.zero;
    IEnumerator DashEffect() {
        GameObject dashParticle = new GameObject();
        SpriteRenderer sprite;
        Color color;

        dashParticle.name = "dashParticle";
        dashParticle.transform.position = transform.GetChild(0).position;
        dashParticle.transform.localScale = this.transform.localScale;
        dashParticle.AddComponent<SpriteRenderer>();

        sprite = dashParticle.GetComponent<SpriteRenderer>();
        sprite.sprite = GetComponentInChildren<SpriteRenderer>().sprite;

        color = sprite.color;
        color.a = .25f;
        sprite.color = color;

        Destroy(dashParticle, .25f);

        yield return new WaitForSeconds(_dashTime / 7);

        if (rb.velocity != Vector2.zero)
            StartCoroutine(DashEffect());
    }
    IEnumerator DashTimer() {
        if (_availableDashes >= _maxDashes)
            yield return null;
        yield return new WaitForSeconds(_dashCooldown);
        _availableDashes++;
    }
}
