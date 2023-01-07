using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {
    [Header("HEALTH")]
    public float Health;
    public float MaxHealth;
    [Header("MOVEMENT")]
    public float Speed;
    public float DashForce;
    public float DashTime;
    public float DashCooldown;
    public int MaxDashes;
    [Header("DAMAGE")]
    public float BasicCooldown;
    public float Basic1Damage;
    public float Basic2Damage;


    InputManager _inputManager;
    public void Initialize(InputManager inputManager) { 
        _inputManager= inputManager;
        Health = MaxHealth;
    }
}
