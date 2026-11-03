using UnityEngine;

public class CashCollection : MonoBehaviour
{
    public int moneyPerProduct = 10; //Money earned for each product
    public MoneyManager moneyManager; //Reference to the money manager

    //Runs when another collider enters the collection area
    void OnTriggerEnter2D(Collider2D other)
    {
        //Checks if the object entering is a product
        if (other.CompareTag("Product"))
        {
            // Add money to the player's total
            moneyManager.AddMoney(moneyPerProduct);

            //Adds money for the product
            Debug.Log("Earned £" + moneyPerProduct);

            //Destroy the product after collecting it
            Destroy(other.gameObject);
        }
    }
}
