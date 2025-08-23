using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject skeletonPrefab;
    private float timer;
    public Vector2 skeletonCount;
    public Vector2 skeletonCooldown;
    private float random;
    private int randomCount;
    // Start is called before the first frame update
    void Start()
    {
        random = Random.Range(skeletonCooldown.x, skeletonCooldown.y);
        randomCount=Random.Range((int)skeletonCount.x, (int)skeletonCount.y);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer>random) 
        {
            timer = 0;
            random = Random.Range(skeletonCooldown.x, skeletonCooldown.y);
            for (int i = 0; i < randomCount; i++)
            {
                Instantiate(skeletonPrefab, transform.position, Quaternion.identity);

            }
        }
    }
}
