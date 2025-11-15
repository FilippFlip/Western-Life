using UnityEngine;

public class LosePoint : MonoBehaviour
{
    public int loseCount;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out EnemyController en))
        {
            loseCount++;
            Destroy(en.gameObject);
            if (loseCount >= 5)
            {
                //gameover//
            }
        }
    }
}
