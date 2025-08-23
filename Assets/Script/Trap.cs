using UnityEngine;

public class Trap : MonoBehaviour
{
    public ParticleSystem explosionEffect;
    private bool isTriggered;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private async void OnTriggerEnter(Collider other)
    {
        if (other.tag == "skelet"&&isTriggered==false)
        {
            isTriggered = true;
            explosionEffect.Play();
            await Awaitable.WaitForSecondsAsync(10);
            Destroy(gameObject);
        }
    }
}
