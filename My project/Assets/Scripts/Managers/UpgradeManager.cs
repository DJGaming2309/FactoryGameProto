
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    [Header("References")]
    public MoneyManager moneyManager;
    public CashCollection cashCollection;

    [Header("Upgrade Button")]
    public Button upgradeButton;
    public TextMeshProUGUI upgradeButtonText;

    [Header("Upgrade Settings")]
    public int upgradeCost = 100;
    public int priceIncrease = 10;
    public int costIncrease = 100;
    public int maximumUpgradeLevel = 5;

    // Tracks how many upgrades have been purchased.
    private int upgradeLevel = 0;

    void Start()
    {
        UpdateUpgradeButton();
    }

    // Called when the player clicks the upgrade button.
    public void BuyUpgrade()
    {
        // Stop if the maximum level has been reached.
        if (upgradeLevel >= maximumUpgradeLevel)
            return;

        // Check whether the player can afford the upgrade.
        if (moneyManager.money < upgradeCost)
            return;

        // Deduct the upgrade cost.
        moneyManager.money -= upgradeCost;
        moneyManager.UpdateMoneyDisplay();

        // Increase the value earned from each product.
        cashCollection.moneyPerProduct += priceIncrease;

        // Increase the upgrade level.
        upgradeLevel++;

        // Increase the cost of the next upgrade.
        upgradeCost += costIncrease;

        // Refresh the button.
        UpdateUpgradeButton();
    }

    // Updates the upgrade button's text and availability.
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
