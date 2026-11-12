using UnityEngine;
using TMPro;

public class MoneyManager : MonoBehaviour
{
    public int money = 0; //how much money to start with
    public TextMeshProUGUI moneyText; //shows current money

    //starts with the current money
    void Start()
    {
        UpdateMoneyDisplay();
    }

    //adds money to the player's total.
    public void AddMoney(int amount)
    {
        money += amount;
        UpdateMoneyDisplay();
    }

    //attempts to spend money.
    //returns true if the purchase works.
    public bool SpendMoney(int amount)
    {
        if (amount < 0 || money < amount)
            return false;

        money -= amount;
        UpdateMoneyDisplay();

        return true;
    }

    //updates the money counter on screen.
    public void UpdateMoneyDisplay()
    {
        if (moneyText != null)
            moneyText.text = "£" + money;
    }
}
