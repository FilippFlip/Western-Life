using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dynamite : Bullet
{
    public float explosionRadius;
    private Collider[] skeletons;
    // Start is called before the first frame update
    async void Start()
    {
        await Awaitable.WaitForSecondsAsync(lifeTime);
        Instantiate(impact, transform.position, Quaternion.identity);
        skeletons=Physics.OverlapSphere(transform.position, explosionRadius);
        foreach(Collider c in skeletons)
        {
            if (c.TryGetComponent(out EnemyController enemy))
            {
                enemy.Death();
            }

        }
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        
    }
}
