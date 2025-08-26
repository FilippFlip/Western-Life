using UnityEngine;

public class Trap : MonoBehaviour
{
    public float explosionRadius;
    private Collider[] skeletons;
    public ParticleSystem explosionEffect;
    private bool isTriggered;

    public AudioClip buildingSound;
    public AudioClip explosionSound;
    public AudioClip afterEffectSound;

    public AudioSource audioSource;
    private void Start()
    {
        audioSource.clip = buildingSound;
        audioSource.Play();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private async void OnTriggerEnter(Collider other)
    {

        if (other.tag == "skelet"&&isTriggered==false)
        {
            audioSource.clip = explosionSound;
            audioSource.Play();

            skeletons = Physics.OverlapSphere(transform.position, explosionRadius);
            foreach (Collider c in skeletons)
            {

                if (c.TryGetComponent(out EnemyController enemy))
                {
                    enemy.Death();
                }

            }
            isTriggered = true;
            explosionEffect.Play();
            await Awaitable.WaitForSecondsAsync(2);
            audioSource.clip = afterEffectSound;
            audioSource.Play();
            await Awaitable.WaitForSecondsAsync(10);
            Destroy(gameObject);

        }
    }
}
