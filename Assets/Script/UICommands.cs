using TMPro;
using UnityEngine;

public class UICommands : MonoBehaviour
{
    public GameObject panel;
    public GameObject trapPrefab;
    public GameObject player;
    public GameObject knightPrefab;
    public int money;
    public TMP_Text moneyText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moneyText.text = money.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            
            panel.SetActive(!panel.activeSelf);
            if (panel.activeSelf)
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
        if (Input.GetKeyDown(KeyCode.E)&&money>=10)
        {
            money = money - 10;
            moneyText.text = money.ToString();
            Instantiate(trapPrefab, player.transform.position + player.transform.forward*3+Vector3.down, Quaternion.identity);

        }
        if (Input.GetKeyDown(KeyCode.Q)&&money>=25)
        {
            money = money - 25;
            moneyText.text = money.ToString();
            Instantiate(knightPrefab, player.transform.position+player.transform.forward*3, Quaternion.identity);

        }
    }
    public void Attack()
    {

    }
    public void Defend()
    {

    }

}
