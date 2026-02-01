using UnityEngine;

public class Health : MonoBehaviour
{
    public void TakeDamage(float damage, Vector3 hitDir, float knockForce)
    {
        EnemyStats stats = GetComponent<EnemyStats>();
        stats.currentHealth -= damage;

        if (TryGetComponent(out Knockback kb))
            kb.Apply(hitDir, knockForce);

        if (stats.currentHealth <= 0)
            Destroy(gameObject);
    }
}
