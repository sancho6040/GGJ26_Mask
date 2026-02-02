using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    public float knockForce = 5f;

    float lastHit;

    public void Hit(Transform target)
    {
        if (Time.time < lastHit + 1f) return;
        lastHit = Time.time;

        if (target.TryGetComponent(out Health hp))
        {
            Vector3 dir = (target.position - transform.position);
            hp.TakeDamage(GetComponent<EnemyStats>().damage, dir, knockForce);
        }
    }

}
