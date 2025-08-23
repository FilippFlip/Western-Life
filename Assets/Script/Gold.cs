using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Gold : MonoBehaviour
{
    public bool taken=false;
    public float distance = 10;
    private Rigidbody rb;
    private void Update()
    {
        if (transform.position.y<-100)
        {
            Destroy(gameObject);
        }
        NavMesh.SamplePosition(transform.position, out NavMeshHit hit, distance, NavMesh.AllAreas);
        if (hit.distance >= 0.5)
        {
            rb.linearVelocity = Vector3.zero;
            transform.position = hit.position + Vector3.up * 0.15f;
            
        }
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody>();

    }
}



    

