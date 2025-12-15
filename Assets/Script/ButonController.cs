using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButonController : MonoBehaviour
{
    public SceneAsset scene;
    private Image image;
    private SceneState stater;

    private void Start()
    {
        image = GetComponent<Image>();
        Cursor.lockState = CursorLockMode.None;
        var manager = FindAnyObjectByType<GameManager>();
        foreach (SceneState state in manager.states)
        {
            if (state.scene.name == scene.name)
            {
                state.buttonImage = image;
                this.stater = state;
            }
        }
    }
    public void Load()
    {
        if (stater.locked==false)
        {
            SceneManager.LoadScene(scene.name);
        }
        
    }
}
