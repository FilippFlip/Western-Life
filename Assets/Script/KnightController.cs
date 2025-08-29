using UnityEngine;
using UnityEngine.AI;

public class KnightController : MonoBehaviour
{
    public float detectionRadius = 5f;
    private NavMeshAgent agent;
    private Vector3 targetPosition;
    private EnemyController currentEnemy;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    public void SetTarget(Vector3 position)
    {
        targetPosition = position;
        currentEnemy = null;
        agent.SetDestination(targetPosition);
    }

    void Update()
    {
        DetectEnemies();

        if (currentEnemy != null)
        {
            agent.SetDestination(currentEnemy.transform.position);
        }
        else
        {
            agent.SetDestination(targetPosition);
        }
    }

    private void DetectEnemies()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius);

        EnemyController nearestEnemy = null;
        float nearestDistance = Mathf.Infinity;

        foreach (var col in colliders)
        {
            EnemyController enemy = col.GetComponent<EnemyController>();
            if (enemy != null)
            {
                float dist = Vector3.Distance(transform.position, enemy.transform.position);
                if (dist < nearestDistance)
                {
                    nearestDistance = dist;
                    nearestEnemy = enemy;
                }
            }
        }

        currentEnemy = nearestEnemy;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
