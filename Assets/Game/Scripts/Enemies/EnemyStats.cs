using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public EnemyType type;

    public float maxHealth = 50f;
    public float damage = 10f;
    public float moveSpeed = 3f;
    public float attackRange = 1.5f;
    public float attackCooldown = 1f;

    [HideInInspector] public float currentHealth;

    void Awake()
    {
        currentHealth = maxHealth;
    }
}
