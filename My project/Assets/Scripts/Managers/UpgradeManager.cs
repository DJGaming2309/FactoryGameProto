
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    public MoneyManager moneyManager; //Reference to the player's money
    public CashCollection cashCollection; //Reference to the selling area

    public Button upgradeButton; //The upgrade button
    public TextMeshProUGUI upgradeButtonText; //The button's text

    public int upgradeCost = 50; //Cost of the upgrade
    public int priceIncrease = 10; //Extra money earned per product

    //Runs when the game starts
    void Start()
    {
        UpdateUpgradeButton();
    }

    //Runs when the player clicks the upgrade button
    public void BuyUpgrade()
    {
        //Check if the player has enough money
        if (moneyManager.money >= upgradeCost)
        {
            //Remove the upgrade cost from the player's money
            moneyManager.money -= upgradeCost;

            //Increase the money earned per product
            cashCollection.moneyPerProduct += priceIncrease;

            //Update the money counter
            moneyManager.UpdateMoneyDisplay();

            //Disable the button after purchasing
            upgradeButton.interactable = false;

            //Update the button text
            upgradeButtonText.text = "Upgrade Purchased!";
        }
    }

    //Updates the upgrade button
    void UpdateUpgradeButton()
    {
        upgradeButtonText.text = "Upgrade Product Value (£" + upgradeCost + ")";
    }
}
