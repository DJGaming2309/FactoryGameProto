using Abertay.Analytics;
using System.Collections.Generic;
using UnityEngine;

public class CashCollection : MonoBehaviour
{
    public int moneyPerProduct = 10; //money earned for each product
    public MoneyManager moneyManager; //reference to the money manager game object

    //runs when another collider enters the collection area
    void OnTriggerEnter2D(Collider2D other)
    {
        //checks if the game object entering is a product
        if (other.CompareTag("Product"))
        {
            //adds money to the player's total
            moneyManager.AddMoney(moneyPerProduct);

            //tracks how many prodocts were collected
            SessionAnalytics.TrackProductCollected(moneyPerProduct);

            //records the collection event in Abertay Analytics.
            AnalyticsManager.SendCustomEvent(
                "ProductCollected",
                new Dictionary<string, object>
                {
                    { "ProductValue", moneyPerProduct }
                }
            );


            //adds money for the product
            Debug.Log("Earned £" + moneyPerProduct);

            //destroys the product after collecting it
            Destroy(other.gameObject);
        }
    }
}
