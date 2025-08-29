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
    void Start()
    {
        
        agent = GetComponent<NavMeshAgent>();
        ragdoll.AddRange(GetComponentsInChildren<Collider>());
        rbs.AddRange(GetComponentsInChildren<Rigidbody>());
        ragdoll.Remove(GetComponent<Collider>());
        parentCollider = GetComponent<Collider>();  

    }
    void Update()
    {
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

}
