using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoldCountUi : MonoBehaviour
{
    public int GoldCount;
    public TMP_Text text;
    public AudioClip battleMusic;
    public AudioClip gameOverMusic;
    public AudioSource sorce;
    private float timer;
    public TMP_Text timeText;
   

    // Update is called once per frame
    void Update()
    {
        
            GoldCount = FindObjectsByType<Gold>(FindObjectsSortMode.None).Length;
            text.text = GoldCount.ToString();
        if (GoldCount <= 0)
        {
            SceneManager.LoadScene("Lose");
        }
        
        timer += Time.deltaTime;
        timeText.text = (Mathf.Round(timer * 10) / 10).ToString();

        if (timer >= 200)
        {
            SceneManager.LoadScene("Win");

        }
    }

}
