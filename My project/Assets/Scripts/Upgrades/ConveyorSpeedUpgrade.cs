
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ConveyorSpeedUpgrade : MonoBehaviour
{
    [Header("References")]
    public MoneyManager moneyManager;

    [Header("Upgrade Button")]
    public Button upgradeButton;
    public TextMeshProUGUI upgradeButtonText;

    [Header("Upgrade Settings")]
    public int upgradeCost = 150;
    public int costIncrease = 100;
    public float speedIncrease = 0.5f;
    public int maximumUpgradeLevel = 5;

    // Tracks the number of upgrades purchased.
    private int upgradeLevel = 0;

    private void Start()
    {
        UpdateUpgradeButton();
    }

    // Called when the player clicks the upgrade button.
    public void BuyConveyorSpeedUpgrade()
    {
        // Stop if maximum level has been reached.
        if (upgradeLevel >= maximumUpgradeLevel)
            return;

        // Check whether the player can afford the upgrade.
        if (!moneyManager.SpendMoney(upgradeCost))
            return;

        // Increase the speed of every conveyor.
        ConveyorBelt.speedMultiplier += speedIncrease;

        // Increase the upgrade level.
        upgradeLevel++;

        // Increase the next upgrade cost.
        upgradeCost += costIncrease;

        // Refresh the button.
        UpdateUpgradeButton();
    }

    // Updates the upgrade button.
    private void UpdateUpgradeButton()
    {
        if (upgradeLevel >= maximumUpgradeLevel)
        {
            upgradeButtonText.text = "MAX LEVEL REACHED!";
            upgradeButton.interactable = false;
        }
        else
        {
            upgradeButtonText.text =
                "Upgrade Conveyor Speed (£" + upgradeCost + ")";

            upgradeButton.interactable = true;
        }
    }
}
