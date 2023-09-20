using UnityEngine; 
public class Damager : MonoBehaviour
{
    float _value;
    public DOTInfo _dotInfo;
    public void Initialize(float value, DOTInfo info) {
        _value= value;
        _dotInfo = info;
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag(this.gameObject.tag))
            return;
        if (collision.gameObject.GetComponent<Health>() as Health != null)
            Damage(collision.gameObject.GetComponent<Health>());
    }
    private void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.CompareTag(this.gameObject.tag))
            return;
        if (collider.gameObject.GetComponent<Health>() as Health != null)
            Damage(collider.gameObject.GetComponent<Health>());
    }
    public void Damage(Health health) {
        health.GetDamage(_value, transform); 

        if (health != null && health.DOTReciever != null)
            health.DOTReciever.RecieveDOT(_dotInfo); 
    }
} 