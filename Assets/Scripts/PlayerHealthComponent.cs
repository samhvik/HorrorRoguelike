using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class PlayerHealthComponent : MonoBehaviour
{
    [SerializeField]
    private float _health;

    private bool invulnerable = false;
    
    [SerializeField]
    private float invulnerabilityDurationSeconds;

    public UnityEvent<PlayerHealthComponent> OnHealthChange;

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
        
    }

    public void TakeDamage(float amount){
        if(!invulnerable){
            _health -= Mathf.Clamp(amount, 0, health);
            StartCoroutine(BecomeInvulnerable());
            FireOnHealthChange();
        }
    }

    private void FireOnHealthChange(){
        OnHealthChange?.Invoke(this);
    }

    private IEnumerator BecomeInvulnerable(){
        Debug.Log("Player now invulnerable");

        invulnerable = true;
        yield return new WaitForSeconds(invulnerabilityDurationSeconds);
        invulnerable = false;
        Debug.Log("Player no longer invulnerable");

    }
}
