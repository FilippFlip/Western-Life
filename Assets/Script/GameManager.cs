using System;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public SceneState tutorial = new();
    public SceneState town = new();
    public SceneState canyon = new();
    public SceneState celebration = new();

    public Sprite unlockedImage;
    public Sprite lockedImage;
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        canyon.locked = true;
        town.locked = true;
        celebration.locked = true;
        tutorial.locked = false;
    }

    void Update()
    {
        if (tutorial.isFinished)
        {
            town.locked = false;
            canyon.locked=false;
            
        }
        if (town.isFinished && canyon.isFinished)
        {
            celebration.locked = false;
        }
        tutorial.buttonImage.sprite=tutorial.locked?lockedImage: unlockedImage;
        town.buttonImage.sprite = town.locked ? lockedImage : unlockedImage;
        canyon.buttonImage.sprite = canyon.locked ? lockedImage : unlockedImage;
        celebration.buttonImage.sprite = celebration.locked ? lockedImage : unlockedImage;
    }
    
}
[Serializable]
public class SceneState
{
    public bool isFinished;
    public bool locked;
    public Image buttonImage;
    
}