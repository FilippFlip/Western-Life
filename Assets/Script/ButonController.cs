using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButonController : MonoBehaviour
{
    public string menu = "MainMenu";

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }
    public void Load()
    {
        SceneManager.LoadScene(menu);

    }
}
