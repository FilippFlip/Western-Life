using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatsHandler : MonoBehaviour
{
    public int maxHp;
    public int currentHp;
    public Image healthBar;
    public TMP_Text healthText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeHealth(int change)
    {
        currentHp = currentHp + change;
        if (currentHp>maxHp)
        {
            currentHp=maxHp;
        }
        if (currentHp <= 0)
        {
            currentHp = 0;
        }
        healthBar.fillAmount = (float)currentHp / maxHp;
        healthText.text= ((float)currentHp / maxHp).ToString()+"%";
    }
}
