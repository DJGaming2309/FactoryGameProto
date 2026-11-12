
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ConveyorSpeedUpgrade : MonoBehaviour
{
    //finds money manager
    [Header("References")]
    public MoneyManager moneyManager;

    //finds upgrade button and text
    [Header("Upgrade Button")]
    public Button upgradeButton;
    public TextMeshProUGUI upgradeButtonText;

    //what the upgrade does
    [Header("Upgrade Settings")]
    public int upgradeCost = 150;
    public int costIncrease = 100;
    public float speedIncrease = 0.5f;
    public int maximumUpgradeLevel = 5;

    //tracks the number of upgrades purchased.
    private int upgradeLevel = 0;

    private void Start()
    {
        UpdateUpgradeButton();
    }

    //called when the player clicks the upgrade button.
    public void BuyConveyorSpeedUpgrade()
    {
        //stops if maximum level has been reached.
        if (upgradeLevel >= maximumUpgradeLevel)
            return;

        //checks whether the player can afford the upgrade.
        if (!moneyManager.SpendMoney(upgradeCost))
            return;

        //increases the speed of every conveyor.
        ConveyorBelt.speedMultiplier += speedIncrease;

        //increases the upgrade level.
        upgradeLevel++;

        //increases the next upgrade cost.
        upgradeCost += costIncrease;

        //refreshs the button.
        UpdateUpgradeButton();
    }

    //updates the upgrade button.
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
