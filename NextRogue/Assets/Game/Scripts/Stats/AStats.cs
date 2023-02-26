using System.Collections;
using UnityEngine;

public abstract class AStats : MonoBehaviour
{
    [Header("DETAILS")]
    public string Name;
    public string ShortDescription;
    public string Description;
    [Header("HEALTH")]
    public float Health;
    public float MaxHealth;
    [Header("MOVEMENT")]
    public float Speed;
    float _speed;

    public virtual void Initialize() {
        Health = MaxHealth;
        _speed= Speed;
    }
    public IEnumerator Clamp() {
        Speed = 0;
        yield return new WaitForSeconds(.5f);
        Speed = _speed;
    }
    
}
