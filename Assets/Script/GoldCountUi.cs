using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class GoldCountUi : MonoBehaviour
{
    public bool isTimerWinCondition;
    public int GoldCount;
    public TMP_Text text;
    public AudioClip battleMusic;
    public AudioClip gameOverMusic;
    public AudioSource sorce;
    private float timer;
    public TMP_Text timeText;
    public static List<Gold> AllGold = new List<Gold>();
    
    public static event Action OnGoldListChanged;
    public async void Awake()
    {
        GoldCount += 1;
        await Awaitable.WaitForSecondsAsync(1);
        GoldCount -= 1;
    }
    private void OnEnable()
    {
        Gold.OnGoldSpawned += AddGold;
        Gold.OnGoldDestroyed += RemoveGold;
    }

    private void OnDisable()
    {
        Gold.OnGoldSpawned -= AddGold;
        Gold.OnGoldDestroyed -= RemoveGold;
    }

    private void AddGold(Gold g)
    {
        AllGold.Add(g);
        GoldCount = AllGold.Count;
        OnGoldListChanged?.Invoke();
    }

    private void RemoveGold(Gold g)
    {
        AllGold.Remove(g);
        GoldCount = AllGold.Count;
        OnGoldListChanged?.Invoke();
    }
    void Update()
    {
        if (isTimerWinCondition)
        {
            text.text = GoldCount.ToString();
            if (GoldCount <= 0)
            {
                
                SceneManager.LoadScene("Lose");
            }
            timer += Time.deltaTime;
            timeText.text = (Mathf.Round(timer * 10) / 10).ToString();

            if (timer >= 200)
            {
                FindAnyObjectByType<GameManager>().town.isFinished = true;
                SceneManager.LoadScene("Win");

            }
        }
    }

}
