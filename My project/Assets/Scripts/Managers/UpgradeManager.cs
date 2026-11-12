
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using Abertay.Analytics;

public class UpgradeManager : MonoBehaviour
{
    //game objects to look at.
    [Header("References")]
    public MoneyManager moneyManager;
    public CashCollection cashCollection;

    //what upgrade button to look at.
    [Header("Upgrade Button")]
    public Button upgradeButton;
    public TextMeshProUGUI upgradeButtonText;

    //what the upgrade does in game
    [Header("Upgrade Settings")]
    public int upgradeCost = 100;
    public int priceIncrease = 10;
    public int costIncrease = 100;
    public int maximumUpgradeLevel = 5;

    //tracks how many upgrades have been purchased.
    private int upgradeLevel = 0;

    void Start()
    {
        UpdateUpgradeButton();
    }

    //called when the player clicks the upgrade button.
    public void BuyUpgrade()
    {
        //stops if the maximum level has been reached.
        if (upgradeLevel >= maximumUpgradeLevel)
            return;

        //checks whether the player can afford the upgrade.
        if (moneyManager.money < upgradeCost)
            return;

        //takes away the upgrade cost.
        moneyManager.money -= upgradeCost;
        moneyManager.UpdateMoneyDisplay();

        //increases the value earned from each product.
        cashCollection.moneyPerProduct += priceIncrease;

        //increases the upgrade level.
        upgradeLevel++;

        //tracks how many upgrades were bought
        SessionAnalytics.TrackUpgradePurchased();

        //increases the cost of the next upgrade.
        upgradeCost += costIncrease;

        //records when an upgrade is purchased.
        AnalyticsManager.SendCustomEvent(
            "UpgradePurchased",
            new Dictionary<string, object>
            {
                { "UpgradeType", "ProductValue" },
                { "UpgradeLevel", upgradeLevel },
                { "UpgradeCost", upgradeCost - costIncrease },
                { "ProductValue", cashCollection.moneyPerProduct }
            }
        );

        //refreshs the button.
        UpdateUpgradeButton();
    }

    //updates the upgrade button's text and availability.
    void UpdateUpgradeButton()
    {
        if (upgradeLevel >= maximumUpgradeLevel)
        {
            upgradeButtonText.text = "MAX LEVEL REACHED!";
            upgradeButton.interactable = false;
        }
        else
        {
            upgradeButtonText.text =
                "Upgrade Product Value (£" + upgradeCost + ")";

            upgradeButton.interactable = true;
        }
    }
}
