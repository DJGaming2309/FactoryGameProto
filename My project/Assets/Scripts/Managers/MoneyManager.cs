
using UnityEngine;
using TMPro;

public class MoneyManager : MonoBehaviour
{
    public int money = 0; // Stores the player's current money
    public TextMeshProUGUI moneyText; // Stores the money counter text

    // Runs when the game starts
    void Start()
    {
        UpdateMoneyDisplay();
    }

    // Adds money to the player's total
    public void AddMoney(int amount)
    {
        money += amount;

        // Update the money counter
        UpdateMoneyDisplay();
    }

    // Updates the text shown on screen
    public void UpdateMoneyDisplay()
    {
        moneyText.text = "£" + money;
    }
}
