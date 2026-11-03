using UnityEngine;

public class ProductionMachine : MonoBehaviour
{
    [Header("Production")]
    public GameObject productPrefab; //The product that the machine creates
    public float productionCooldown = 1.5f; //Time between each production

    [Header("Product Spawn")]
    public Transform spawnPoint; //Where the product appears
    public float popForce = 3f; //How strongly the product pops out

    private float nextProductionTime; //When the machine can produce again

    //Runs when the player clicks the machine
    void OnMouseDown()
    {
        //Check if the machine is still on cooldown
        if (Time.time < nextProductionTime)
            return;

        //Create a new product
        ProduceProduct();

        //Set the next time the machine can produce
        nextProductionTime = Time.time + productionCooldown;
    }

    //Creates and launches a product
    void ProduceProduct()
    {
        //Spawn the product at the spawn point
        GameObject product = Instantiate(productPrefab,spawnPoint.position,Quaternion.identity);

        //Get the product's Rigidbody2D component
        Rigidbody2D rb = product.GetComponent<Rigidbody2D>();

        //Check that the product has physics attached
        if (rb != null)
        {
            //Make the product pop upwards and to the right
            rb.AddForce(new Vector2(popForce, popForce),ForceMode2D.Impulse);
        }
    }

    //Changes how quickly the machine produces products
    public void UpgradeProductionSpeed(float reduction)
    {
        productionCooldown -= reduction;

        //Rounds the cooldown to one decimal place
        productionCooldown = Mathf.Round(productionCooldown * 10f) / 10f;

        //Prevents the machine from becoming too fast
        productionCooldown = Mathf.Max(0.1f, productionCooldown);
    }
}