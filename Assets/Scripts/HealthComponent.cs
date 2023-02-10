using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] private float health = 100;
    [SerializeField] private float maxHealth = 100;

    [SerializeField] private float invulnerabilityDuration = 2;
    private bool invulnerable;

    public event EventHandler OnHealthChanged;
    public event EventHandler OnDeath;
    public event EventHandler OnDamaged;
    public event EventHandler OnHealed;

    private void Start()
    {
        ResetHealth();
    }

    public float GetHealth()
    {
        return health;
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }

    public float GetHealthNormalized()
    {
        return health / maxHealth;
    }

    public void SetMaxHealth(float newMaxHealth, bool reset = false)
    {
        maxHealth = newMaxHealth;
        if (reset)
        {
            ResetHealth();
        }
    }

    public void ResetHealth()
    {
        health = maxHealth;
        OnHealthChanged?.Invoke(this, EventArgs.Empty);
        OnHealed?.Invoke(this, EventArgs.Empty);
    }

    public void Damage(float damage)
    {
        if (invulnerable || IsDead())
        {
            return;
        }

        health -= damage;
        OnHealthChanged?.Invoke(this, EventArgs.Empty);
        OnDamaged?.Invoke(this, EventArgs.Empty);

        if (health <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(BecomeInvulnerable());
        }
    }

    public void MortalDamage(float damage)
    {
        if (IsDead())
        {
            return;
        }

        health -= damage;
        OnHealthChanged?.Invoke(this, EventArgs.Empty);
        OnDamaged?.Invoke(this, EventArgs.Empty);

        if (health <= 0)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        health = Mathf.Min(health + amount, maxHealth);
        OnHealthChanged?.Invoke(this, EventArgs.Empty);
        OnHealed?.Invoke(this, EventArgs.Empty);
    }

    public bool IsDead()
    {
        return health <= 0;
    }

    private void Die()
    {
        OnDeath?.Invoke(this, EventArgs.Empty);
    }

    private IEnumerator BecomeInvulnerable()
    {
        invulnerable = true;
        yield return new WaitForSeconds(invulnerabilityDuration);
        invulnerable = false;
    }

    private void OnTriggerStay(Collider other)
    {
        // Removed for brevity
    }

    private void OnTriggerEnter(Collider other)
    {
        // Removed for brevity
    }
}
