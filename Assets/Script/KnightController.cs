using UnityEngine;
using UnityEngine.AI;

public class KnightController : MonoBehaviour
{
    public float detectionRadius = 5f;
    private NavMeshAgent agent;
    private Transform target;
    private EnemyController currentEnemy;
    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    public void SetTarget(Transform position)
    {
        target = position;
        currentEnemy = null;
        agent.SetDestination(target.position);
    }

    void Update()
    {
        DetectEnemies();
        AnimationController();
        if (currentEnemy != null)
        {
            agent.SetDestination(currentEnemy.transform.position);
        }
        else
        {
            agent.SetDestination(target.position);
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
    private void AnimationController()
    {
        if (target == null && currentEnemy == null)
        {
            animator.SetBool("idle", true);
            animator.SetBool("attack", false);
            animator.SetBool("run", false);

        }
        if(target !=null || currentEnemy != null)
        {
            var distance = Vector3.Distance(transform.position, currentEnemy.transform.position);
            if (currentEnemy != null && distance >= 1)
            {
                animator.SetBool("idle", false);
                animator.SetBool("attack", false);
                animator.SetBool("run", true);
            }
        }

    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }

}
