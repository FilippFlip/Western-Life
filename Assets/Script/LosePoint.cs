using UnityEngine;
using UnityEngine.SceneManagement;

public class LosePoint : MonoBehaviour
{
    public int loseCount;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out EnemyController en))
        {
            loseCount++;
            Destroy(en.gameObject);
            if (loseCount >= 5||en.killingReward>=50)
            {

                SceneManager.LoadScene("Lose");
            }

        }
    }
}
