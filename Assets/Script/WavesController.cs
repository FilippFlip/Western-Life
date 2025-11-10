using System;
using UnityEngine;

public class WavesController : MonoBehaviour
{
    public Wave[] w;
    public Transform[] s;
    private async void Start()
    {
        await SpawnWave(w[0]);
        await SpawnWave(w[1]);
        await SpawnWave(w[2]);
        await SpawnWave(w[3]);
        await SpawnWave(w[4]);
        await SpawnWave(w[5]);
        await SpawnWave(w[6]);
        await SpawnWave(w[7]);
        await SpawnWave(w[8]);
        await SpawnWave(w[9]);
        await SpawnWave(w[10]);       

    }
    private async Awaitable SpawnWave(Wave wave)
    {
        for (int i = 0; i < wave.enemyCount; i++)
        {
            await Awaitable.WaitForSecondsAsync(wave.spawnSpeed);
            int random = UnityEngine.Random.Range(0, 3);

            Vector3 position = s[random].position;
            Instantiate(wave.enemyPrefab, position, Quaternion.identity);

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

