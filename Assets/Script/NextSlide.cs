using UnityEngine;
using UnityEngine.SceneManagement;

public class NextSlide : MonoBehaviour
{
    public GameObject activate;
    public GameObject deactivate;
    public bool lastSlide;
    
    public void Slides ()
    {
        GetComponent<AudioSource>().Play();
        activate.SetActive (true);
        deactivate.SetActive (false);
        if (lastSlide==true)
        {
            FindAnyObjectByType<GameManager>().tutorial.isFinished = true;
            SceneManager.LoadScene("MainMenu");
        }
    }
     


}
