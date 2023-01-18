using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System;

public class PlayerHealthComponent : MonoBehaviour
{
    [SerializeField]
    private float _health;

    private bool invulnerable = false;
    
    [SerializeField]
    private float invulnerabilityDurationSeconds;

    public Action OnHealthChange;
    public Action OnDeath;

    private PlayerStateMachine stateMachine;

    public float health{
        get { return _health; }
        set { 
            if(!invulnerable && _health != value) {
                _health = value;
                StartCoroutine(BecomeInvulnerable());
                FireOnHealthChange();
            } 
        }
    }

    void Start(){
        stateMachine = GetComponent<PlayerStateMachine>();
    }

    void Update(){
        if(_health <= 0){
            _health = 0;
            FireOnDeath();
        }
    }

    public void TakeDamage(float amount){
        if(!invulnerable && _health > 0){
            _health -= Mathf.Clamp(amount, 0, health);
            StartCoroutine(BecomeInvulnerable());
            FireOnHealthChange();
        }
    }

    private void FireOnHealthChange(){
        OnHealthChange?.Invoke();
    }

    private IEnumerator BecomeInvulnerable(){
        Debug.Log("Player now invulnerable");

        invulnerable = true;
        yield return new WaitForSeconds(invulnerabilityDurationSeconds);
        invulnerable = false;
        Debug.Log("Player no longer invulnerable");

    }

    public void FireOnDeath(){
        OnDeath?.Invoke();
    }

    private void OnTriggerStay(Collider other){
        if(other.CompareTag("Enemy")){
            this.TakeDamage(other.GetComponent<ProximityDamage>().damage);
        }
    }
}
