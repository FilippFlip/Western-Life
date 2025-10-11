
using System;
using UnityEngine;
using UnityEngine.AI;

public class Gold : MonoBehaviour
{
    public bool taken=false;
    public float distance = 10;
    private float timer;
    public static event Action<Gold> OnGoldSpawned;
    public static event Action<Gold> OnGoldDestroyed;
    
    private void OnEnable()
    {
        OnGoldSpawned?.Invoke(this);
    }

    private void OnDisable()
    {
        OnGoldDestroyed?.Invoke(this);
    }
    private Rigidbody rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (transform.position.y<-100)
        {
            Destroy(gameObject);
        }
        timer+=Time.deltaTime;
        if (timer < 1) return;
        timer = 0;
        NavMesh.SamplePosition(transform.position, out NavMeshHit hit, distance, NavMesh.AllAreas);
        if (hit.distance >= 0.5)
        {
            rb.linearVelocity = Vector3.zero;
            transform.position = hit.position + Vector3.up * 0.15f;
            
        }
    }
    
}



    

