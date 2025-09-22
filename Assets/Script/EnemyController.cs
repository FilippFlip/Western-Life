using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private NavMeshAgent agent;
    private Gold target;
    public List<Collider> ragdoll = new List<Collider>();
    public bool ragdolState;
    public List<Rigidbody> rbs = new List<Rigidbody>();
    private Collider parentCollider;
    public float force;
    private int crab;
    public float detectionRadius;
    private KnightController currentEnemy;
    private Animator animator;
    public float attackRange;

    void Start()
    {      
        agent = GetComponent<NavMeshAgent>();
        ragdoll.AddRange(GetComponentsInChildren<Collider>());
        rbs.AddRange(GetComponentsInChildren<Rigidbody>());
        ragdoll.Remove(GetComponent<Collider>());
        parentCollider = GetComponent<Collider>();  
        animator=GetComponent<Animator>();

    }
    void Update()
    {
        DetectEnemies();
        Navigation();

        if (ragdolState==true)
        {
            foreach (Collider collider in ragdoll)
            {
                collider.enabled = true;
            }
        }
        if (ragdolState == false)
        {
            foreach(Collider collider in ragdoll) 
            { 
                collider.enabled = false; 
            
            }
        }
        foreach(Rigidbody rb in rbs)
        {
            rb.useGravity = ragdolState;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent<Bullet>(out Bullet b))
        {
            Death();
            
        }
        if(collision.gameObject.TryGetComponent<Gold>(out Gold c)&&crab<5)
        {
            
                Destroy(c.gameObject);
                crab++;
                
        }
    }
    public void Death()
    {
        ragdolState = true;
        GetComponent<Animator>().enabled = false;
        agent.enabled = false;
        parentCollider.enabled = false;
        foreach (Collider collider in ragdoll)
        {
            collider.GetComponent<Rigidbody>().AddForce(Vector3.up * force);
        }
        Destroy(gameObject, 10);
    }
    private void Navigation()
    {
        if (currentEnemy != null)
        {
            agent.SetDestination(currentEnemy.transform.position);
            return;
        }
        if (crab >= 5)
        {
            return;
        }
        if (target == null)
        {
            Gold nearest = null;
            float minDistance = Mathf.Infinity;
            Vector3 myPos = transform.position;
            foreach (Gold obj in FindObjectsOfType<Gold>())
            {
                float dist = Vector3.Distance(myPos, obj.transform.position);
                if (dist < minDistance)
                {
                    minDistance = dist;
                    nearest = obj;
                }
            }
            target= nearest;
        }
        

        if (target != null && agent.enabled == true)
        {
            agent.SetDestination(target.transform.position);
            
        }
          
        
    }
    private void DetectEnemies()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius);

        KnightController nearestEnemy = null;
        float nearestDistance = Mathf.Infinity;

        foreach (var col in colliders)
        {
            KnightController enemy = col.GetComponent<KnightController>();
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
        if (target != null || currentEnemy != null)
        {
            var distance = Vector3.Distance(transform.position, currentEnemy.transform.position);
            if (currentEnemy != null && distance >= attackRange)
            {
                animator.SetBool("idle", false);
                animator.SetBool("attack", false);
                animator.SetBool("run", true);
            }
        }
        if (currentEnemy != null)
        {
            var distance = Vector3.Distance(transform.position, currentEnemy.transform.position);
            if (distance < attackRange)
            {
                animator.SetBool("idle", false);
                animator.SetBool("attack", true);
                animator.SetBool("run", false);
            }
        }
    }
}
