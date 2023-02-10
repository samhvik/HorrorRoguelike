using UnityEngine;

public abstract class StatusEffect : MonoBehaviour {

    public HealthComponent target;

    public abstract void Apply();
    public abstract void Remove();
}
