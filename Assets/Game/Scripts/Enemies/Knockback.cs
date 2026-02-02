using UnityEngine;

public class Knockback : MonoBehaviour
{
    public float duration = 0.15f;
    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Apply(Vector3 direction, float force)
    {
        direction.y = 0;
        rb.linearVelocity = Vector3.zero;
        rb.AddForce(direction.normalized * force, ForceMode.Impulse);
    }


    void Stop()
    {
        rb.linearVelocity = Vector3.zero;
    }
}
