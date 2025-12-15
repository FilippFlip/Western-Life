using System;
using System.Collections.Generic;
using UnityEditor;
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

    public List<SceneState> states = new();
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        canyon.locked = true;
        town.locked = true;
        celebration.locked = true;
        tutorial.locked = false;
        
        states.Add(tutorial);
        states.Add(town);
        states.Add(canyon);
        states.Add(celebration);
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
        if(tutorial.buttonImage)
            tutorial.buttonImage.sprite=tutorial.locked?lockedImage: unlockedImage;
        if(town.buttonImage)
            town.buttonImage.sprite = town.locked ? lockedImage : unlockedImage;
        if(canyon.buttonImage)
            canyon.buttonImage.sprite = canyon.locked ? lockedImage : unlockedImage;
        if(celebration.buttonImage)
            celebration.buttonImage.sprite = celebration.locked ? lockedImage : unlockedImage;
    }
}
[Serializable]
public class SceneState
{
    public bool isFinished;
    public bool locked;
    public Image buttonImage;
    public SceneAsset scene;
}