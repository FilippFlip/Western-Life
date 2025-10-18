using System;
using UnityEngine;

public class WavesController : MonoBehaviour
{
    public Wave[] w;
    private async void Start()
    {
        await SpawnWave(w[0]);
        await SpawnWave(w[1]);
        await SpawnWave(w[2]);
        await SpawnWave(w[3]);
        await SpawnWave(w[4]);
        
    }
    private async Awaitable SpawnWave(Wave wave)
    {
        for (int i = 0; i < wave.enemyCount; i++)
        {
            await Awaitable.WaitForSecondsAsync(wave.spawnSpeed);
            Instantiate(wave.enemyPrefab, transform.position, Quaternion.identity);

        }
        await Awaitable.WaitForSecondsAsync(wave.spawnTimeOut);

    }
}
[Serializable]
public class Wave
{
    public GameObject enemyPrefab;
    public int enemyCount;
    public float spawnSpeed;
    public float spawnTimeOut;

}

