using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public Transform shootPoint;
    public GameObject bullet;
    public float bulletSpeed;
    public ParticleSystem fire;
    public Animation _animation;
    public AudioSource audioSource;
    private float timer;
    public float shootCooldown;
    public float recoil;
    public Rigidbody playerRB;
    public float shootDelay;
    public bool abortEffect;
    // Start is called before the first frame update
    void Start()
    {
        playerRB=GetComponentInParent<Rigidbody>();

    }

    // Update is called once per frame
    async void Update()
    {
        timer += Time.deltaTime;
        if (Input.GetMouseButtonDown(0)&&timer>shootCooldown)
        {
            timer = 0;
            fire.Play();         
            _animation.Play();
            audioSource.Play();

            playerRB.AddForce(transform.forward * -recoil);
            await Awaitable.WaitForSecondsAsync(shootDelay);
            if (abortEffect)
            {
                audioSource.Stop();
                fire.Stop();
            }           
            var newBullet = Instantiate(bullet, shootPoint.position, Quaternion.identity);
            newBullet.transform.Rotate(-90, 0, 0);
            newBullet.GetComponent<Rigidbody>().AddForce(shootPoint.forward * bulletSpeed);
            
        }
    }
    
}
