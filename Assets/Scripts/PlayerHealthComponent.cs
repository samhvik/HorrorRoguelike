using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System;

public class PlayerHealthComponent : MonoBehaviour
{
    [SerializeField]
    private float _health;
    [SerializeField]
    private float _healthMax;

    private bool invulnerable = false;
    
    [SerializeField]
    private float invulnerabilityDurationSeconds;

    public event EventHandler OnHealthChanged;
    public event EventHandler OnHealthMaxChanged;
    public event EventHandler OnDamaged;
    public event EventHandler OnHealed;
    public event EventHandler OnDeath;

    private PlayerStateMachine stateMachine;

    public PlayerHealthComponent(float healthMax){
        this._healthMax = healthMax;
        _health = healthMax;
    }

    void Start(){
        stateMachine = GetComponent<PlayerStateMachine>();
    }

    public float Health{
        get { return _health; }
    }

    public float HealthMax{
        get { return _healthMax; }
    }

    public float GetHealthNormalized() {
        return (float)_health / _healthMax;
    }

    public void Damage(float amount){
        if(invulnerable)
            return;
        
        StartCoroutine(BecomeInvulnerable());
        _health -= amount;
        if(Health < 0)
            _health = 0;
        OnHealthChanged?.Invoke(this, EventArgs.Empty);
        OnDamaged?.Invoke(this, EventArgs.Empty);

        if(Health <= 0)
            Die();
    }

    public void Die(){
        OnDeath?.Invoke(this, EventArgs.Empty);
    }

    public bool IsDead(){
        return Health <= 0;
    }

    public void Heal(float amount){
        _health += amount;

        if(Health > HealthMax)
            _health = HealthMax;

        OnHealthChanged?.Invoke(this, EventArgs.Empty);
        OnHealed?.Invoke(this, EventArgs.Empty);
    }

    public void HealComplete(){
        _health = HealthMax;

        OnHealthChanged?.Invoke(this, EventArgs.Empty);
        OnHealed?.Invoke(this, EventArgs.Empty);
    }

    public void SetHealthMax(float newHealthMax, bool fullHealth){
        this._healthMax = newHealthMax;

        if(fullHealth)
            _health = HealthMax;
        
        OnHealthMaxChanged?.Invoke(this, EventArgs.Empty);
        OnHealthChanged?.Invoke(this, EventArgs.Empty);
    }

    private IEnumerator BecomeInvulnerable(){
        Debug.Log("Player now invulnerable");

        invulnerable = true;
        yield return new WaitForSeconds(invulnerabilityDurationSeconds);
        invulnerable = false;
        Debug.Log("Player no longer invulnerable");

    }

    private void OnTriggerStay(Collider other){
        if(other.CompareTag("Enemy")){
            this.Damage(other.GetComponent<ProximityDamage>().damage);
        }
    }
}
