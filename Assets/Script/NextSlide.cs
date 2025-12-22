using UnityEngine;
using UnityEngine.SceneManagement;

public class NextSlide : MonoBehaviour
{
    public GameObject activate;
    public GameObject deactivate;
    public bool lastSlide;
    public AudioSource page;
    public void Slides ()
    {
        page.PlayOneShot(page.clip);
        page.time = 1;       
        activate.SetActive (true);
        deactivate.SetActive (false);
        if (lastSlide==true)
        {
            FindAnyObjectByType<GameManager>().tutorial.isFinished = true;
            SceneManager.LoadScene("MainMenu");
        }
    }
   



}
