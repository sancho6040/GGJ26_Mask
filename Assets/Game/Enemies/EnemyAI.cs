using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private Transform player;
    private EnemyStats stats;
    private float lastAttack;

    void Start()
    {
        stats = GetComponent<EnemyStats>();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    void Update()
    {
        if (!player) return;

        Vector3 a = transform.position;
        Vector3 b = player.position;
        a.y = b.y = 0;

        float distance = Vector3.Distance(a, b);

        if (distance <= stats.attackRange)
            return; // NO avanzar mientras ataca

        if (distance <= 10f)
            Chase();
    }

    void Chase()
    {
        Vector3 dir = (player.position - transform.position);
        dir.y = 0;
        transform.position += dir.normalized * stats.moveSpeed * Time.deltaTime;
    }

    void Attack()
    {
        if (Time.time < lastAttack + stats.attackCooldown) return;
        lastAttack = Time.time;

        if (stats.type == EnemyType.Ranged)
            GetComponent<RangedAttack>().Shoot(player);
        else
            GetComponent<MeleeAttack>().Hit(player);
    }
}