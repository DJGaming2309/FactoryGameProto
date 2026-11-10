using UnityEngine;
using TMPro;

public class MoneyManager : MonoBehaviour
{
    public int money = 0;
    public TextMeshProUGUI moneyText;

    void Start()
    {
        UpdateMoneyDisplay();
    }

    //Adds money to the player's total.
    public void AddMoney(int amount)
    {
        money += amount;
        UpdateMoneyDisplay();
    }

    //Attempts to spend money.
    //Returns true if the purchase succeeds.
    public bool SpendMoney(int amount)
    {
        if (amount < 0 || money < amount)
            return false;

        money -= amount;
        UpdateMoneyDisplay();

        return true;
    }

    //Updates the money counter on screen.
    public void UpdateMoneyDisplay()
    {
        if (moneyText != null)
            moneyText.text = "£" + money;
    }
}
