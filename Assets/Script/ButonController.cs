using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButonController : MonoBehaviour
{
    public SceneAsset scene;
    private Image image;

    private void Start()
    {
        image = GetComponent<Image>();
        Cursor.lockState = CursorLockMode.None;
        var manager = FindAnyObjectByType<GameManager>();
        foreach (var state in manager.states)
        {
            if (state.scene.name == scene.name)
            {
                state.buttonImage = image;
            }
        }
    }
    public void Load()
    {
        SceneManager.LoadScene(scene.name);
    }
}
