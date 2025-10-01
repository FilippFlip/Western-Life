using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatsHandler : MonoBehaviour
{
    public int maxHp;
    public int currentHp;
    public Image healthBar;
    public TMP_Text healthText;
    public Fraction fraction;
    void Start()
    {
        ChangeHealth(0);
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
        healthText.text= ((float)currentHp / maxHp)*100 + "%";
    }
}
