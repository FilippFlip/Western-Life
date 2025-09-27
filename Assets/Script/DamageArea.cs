using UnityEngine;

public class DamageArea : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out StatsHandler stats))
        {
            stats.ChangeHealth(-25);
        }
    }
}
