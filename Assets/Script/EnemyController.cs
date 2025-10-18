using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    private NavMeshAgent agent;
    private Gold target;
    public List<Collider> ragdoll = new List<Collider>();
    public List<Rigidbody> rbs = new List<Rigidbody>();
    private Collider parentCollider;
    public float force;
    private int crab;
    public float detectionRadius;
    private KnightController currentEnemy;
    private Animator animator;
    public float attackRange;
    private StatsHandler statsHandler;
    private float timer;
    public static event Action<int> OnEnemyDeath;
    public int killingReward;
    void Awake()
    {     
        statsHandler=GetComponent<StatsHandler>();
        agent = GetComponent<NavMeshAgent>();
        ragdoll.AddRange(GetComponentsInChildren<Collider>());
        rbs.AddRange(GetComponentsInChildren<Rigidbody>());
        ragdoll.Remove(GetComponent<Collider>());
        parentCollider = GetComponent<Collider>();  
        animator=GetComponent<Animator>();
        ragdoll.RemoveAll(a=> a.gameObject.GetComponent<DamageArea>()!=null);
        SetRagdollState(false);
    }
    private void OnEnable()
    {
        UpdateTarget();
        GoldCountUi.OnGoldListChanged += UpdateTarget;
        statsHandler.OnDeath += Death;
    }
    private void OnDisable()
    {
        GoldCountUi.OnGoldListChanged -= UpdateTarget;
        statsHandler.OnDeath -= Death;
    }
    void Update()
    {
        timer += Time.deltaTime;    
        if (timer >= 0.25f)
        {
            DetectEnemies();
            timer = 0;
        }
        Navigation();
        AnimationController();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent(out Bullet b))
        {
            statsHandler.ChangeHealth(-b.damage);
            
        }
        if(collision.gameObject.TryGetComponent(out Gold c) && crab < 5)
        {
            if (c == target) target = null;
            Destroy(c.gameObject);
            crab++;
        }
    }
    private void SetRagdollState(bool state)
    {
        foreach (var col in ragdoll)
            col.enabled = state;

        foreach (var rb in rbs)
            rb.useGravity = state;
    }
    public async void Death()
    {
        OnEnemyDeath?.Invoke(killingReward);
        statsHandler.enabled = false;
        SetRagdollState(true);
        GetComponent<Animator>().enabled = false;
        agent.enabled = false;
        parentCollider.enabled = false;
        foreach (Collider collider in ragdoll)
        {
            if(collider.TryGetComponent(out StatsHandler _))
            {
                continue;
            }
            collider.GetComponent<Rigidbody>().AddForce(Vector3.up * force);
        }

        await Awaitable.WaitForSecondsAsync(1);
        GetComponentInChildren<Bilboard>().gameObject.SetActive(false);
        Destroy(gameObject, 10);
    }
    private void Navigation()
    {
        if (agent == null || !agent.enabled) return;

        if (currentEnemy != null)
        {
            agent.SetDestination(currentEnemy.transform.position);
            return;
        }
        if(target != null && crab < 5)
            agent.SetDestination(target.transform.position);
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
    private void UpdateTarget()
    {
        if (crab >= 5)
        {
            target = null; 
            return;
        }

        Gold nearest = null;
        float minDist = float.MaxValue;

        for (int i = 0; i < GoldCountUi.AllGold.Count; i++)
        {
            var g = GoldCountUi.AllGold[i];
            float dist = (g.transform.position - transform.position).sqrMagnitude;
            if (dist < minDist)
            {
                minDist = dist;
                nearest = g;
            }
        }

        target = nearest;

        if (target != null)
            agent.SetDestination(target.transform.position);
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
            if (currentEnemy != null)
            {
                var distance = Vector3.Distance(transform.position, currentEnemy.transform.position);
                if (distance >= attackRange)
                {
                    animator.SetBool("idle", false);
                    animator.SetBool("attack", false);
                    animator.SetBool("run", true);
                }
            }
            else
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
