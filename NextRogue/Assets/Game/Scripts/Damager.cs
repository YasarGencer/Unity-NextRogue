using UnityEngine;

public class Damager : MonoBehaviour
{
    float _value;
    public void Initialize(float value) {
        _value= value;
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag(this.gameObject.tag))
            return;
        if (collision.gameObject.GetComponent<Health>() as Health != null)
            collision.gameObject.GetComponent<Health>().GetDamage(_value, transform);
    }
    private void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.CompareTag(this.gameObject.tag))
            return;
        if (collider.gameObject.GetComponent<Health>() as Health != null)
            collider.gameObject.GetComponent<Health>().GetDamage(_value, transform);
    }
}
