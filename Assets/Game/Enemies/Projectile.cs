using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 8f;
    public float damage = 5f;
    public float lifeTime = 3f;

    private Vector3 direction;

    public void Init(Vector3 target)
    {
        direction = (target - transform.position).normalized;
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        direction.y = 0;
        transform.position += direction * speed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.TryGetComponent(out Health hp))
            {
                hp.TakeDamage(damage, direction, 2f);
            }
            Destroy(gameObject);
        }
    }
}
