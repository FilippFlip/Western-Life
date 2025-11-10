using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UICommands : MonoBehaviour
{
    public GameObject panel;
    public GameObject trapPrefab;
    public GameObject player;
    public GameObject knightPrefab;
    public int money;
    public TMP_Text moneyText;
    public float commandRadius = 15;
    public Image panelEarningMoney;
    public int moneyPerSec;
    public float secForMoney;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moneyText.text = money.ToString();
        EnemyController.OnEnemyDeath += (int a) =>
        {
            money += a;
            moneyText.text = money.ToString();
        };

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
                player.GetComponent<PlayerController>().rotationSpeed = 35;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                player.GetComponent<PlayerController>().rotationSpeed = 150;
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
        MoneyOverTime();
    }
    public void Follow()
    {
        var colliders=Physics.OverlapSphere(player.transform.position, commandRadius);
        foreach (var collider in colliders)
        {
            if (collider.TryGetComponent(out KnightController knight))
            {
                knight.followPlayer = true;
            }
        }
    }
    public void Defend()
    {
        var colliders = Physics.OverlapSphere(player.transform.position, commandRadius);
        foreach (var collider in colliders)
        {
            if (collider.TryGetComponent(out KnightController knight))
            {
                knight.followPlayer = false;
            }
        }
    }
    public void MoneyOverTime()
    {
        panelEarningMoney.fillAmount += 1 / secForMoney * Time.deltaTime;
        if (panelEarningMoney.fillAmount==1)
        {
            panelEarningMoney.fillAmount = 0;
            money += moneyPerSec;
            moneyText.text = money.ToString();
        }
    }

}
