using UnityEngine;
using UnityEngine.AI;

public class KnightController : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform attackPoint;
    public Transform defendPoint;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent= GetComponent<NavMeshAgent>();
    }
    // Update is called once per frame
    void Update()
    {

    }
    public void Attack()
    {
        agent.destination = attackPoint.position;
        print(1);
    }
    public void Defend()
    {
        agent.destination= defendPoint.position;
        print(2);
    }
}
