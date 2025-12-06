using UnityEngine;

public class GameOverTrigger : MonoBehaviour
{
    public Canvas win;
    public AudioSource gunShot;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerController>(out var _))
        {
            win.gameObject.SetActive(true);
            gunShot.Play();
        }
    }
}
