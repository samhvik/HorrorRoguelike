using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    public float health { get; private set; } = 100f;

    public float takeDamage(float damage){
        return health -= damage;
    }
}
