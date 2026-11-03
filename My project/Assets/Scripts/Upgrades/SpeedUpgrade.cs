
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SpeedUpgrade : MonoBehaviour
{
    public MoneyManager moneyManager; //Reference to the player's money
    public ProductionMachine productionMachine; //Reference to the production machine

    public Button upgradeButton; //The upgrade button
    public TextMeshProUGUI upgradeButtonText; //The button's text

    public int upgradeCost = 75; //Starting upgrade cost
    public int costIncrease = 50; //Extra cost after each upgrade
    public float speedIncrease = 0.1f; //How much faster production becomes

    private int upgradeLevel = 0; //Current upgrade level
    private const float minimumCooldown = 0.1f; //Fastest possible production speed

    //Runs when the game starts
    void Start()
    {
        UpdateUpgradeButton();
    }

    //Runs when the player clicks the upgrade button
    public void BuySpeedUpgrade()
    {
        //Check if the machine has reached maximum speed
        if (productionMachine.productionCooldown <= minimumCooldown)
            return;

        //Check if the player has enough money
        if (moneyManager.money >= upgradeCost)
        {
            //Remove the upgrade cost from the player's money
            moneyManager.money -= upgradeCost;

            //Update the money counter
            moneyManager.UpdateMoneyDisplay();

            //Increase production speed
            productionMachine.UpgradeProductionSpeed(speedIncrease);

            //Increase the upgrade level
            upgradeLevel++;

            //Increase the next upgrade cost
            upgradeCost += costIncrease;

            // Update the upgrade button
            UpdateUpgradeButton();
        }
    }

    //Updates the upgrade button
    void UpdateUpgradeButton()
    {
        //Check if maximum production speed has been reached
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
