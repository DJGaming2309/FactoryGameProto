
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SpeedUpgrade : MonoBehaviour
{
    [Header("References")]
    public MoneyManager moneyManager; //references to the player's money
    public ProductionMachine productionMachine; //references to the production machine

    [Header("Upgrade Button")]
    public Button upgradeButton; //the upgrade button
    public TextMeshProUGUI upgradeButtonText; //The button's text

    [Header("Upgrade Settings")]
    public int upgradeCost = 75; //starting upgrade cost
    public int costIncrease = 50; //extra cost after each upgrade
    public float speedIncrease = 0.1f; //how much faster production becomes

    private int upgradeLevel = 0; //the current upgrade level
    private const float minimumCooldown = 0.1f; //fastest possible production speed

    //runs when the game starts
    void Start()
    {
        UpdateUpgradeButton();
    }

    //runs when the player clicks the upgrade button
    public void BuySpeedUpgrade()
    {
        //checks if the machine has reached maximum speed
        if (productionMachine.productionCooldown <= minimumCooldown)
            return;

        //checks if the player has enough money
        if (moneyManager.money >= upgradeCost)
        {
            //removes the upgrade cost from the player's money
            moneyManager.money -= upgradeCost;

            //updates the money counter
            moneyManager.UpdateMoneyDisplay();

            //increases production speed
            productionMachine.UpgradeProductionSpeed(speedIncrease);

            //increases the upgrade level
            upgradeLevel++;

            //increases the next upgrade cost
            upgradeCost += costIncrease;

            //updates the upgrade button
            UpdateUpgradeButton();
        }
    }

    //updates the upgrade button
    void UpdateUpgradeButton()
    {
        //checks if maximum production speed has been reached
        if (productionMachine.productionCooldown <= minimumCooldown)
        {
            upgradeButtonText.text = "Maximum Speed Reached!";
            upgradeButton.interactable = false;
        }
        else
        {
            upgradeButtonText.text = "Upgrade Speed (£" + upgradeCost + ")";
            upgradeButton.interactable = true;
        }
    }
}
