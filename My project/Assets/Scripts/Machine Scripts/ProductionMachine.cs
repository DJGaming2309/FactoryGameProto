using UnityEngine;
using System.Collections.Generic;
using Abertay.Analytics;

public class ProductionMachine : MonoBehaviour
{
    //looks for game object and has a preset clicking cooldown
    [Header("Production")]
    public GameObject productPrefab;
    public float productionCooldown = 1.5f;

    //finds the spawn point and pops out the product
    [Header("Product Spawn")]
    public Transform spawnPoint;
    public float popForce = 3f;

    //gives a spawn raduis
    [Header("Spawn Area")]
    public float spawnRadius = 1.5f;
    public float minimumSpawnDistance = 0.5f;

    private float nextProductionTime;

    //runs when the player clicks the machine.
    void OnMouseDown()
    {
        if (Time.time < nextProductionTime)
            return;

        ProduceProduct();

        nextProductionTime = Time.time + productionCooldown;
    }

    //creates and launches a product.
    void ProduceProduct()
    {
        if (productPrefab == null)
            return;

        //uses the spawn point as the centre of the spawn area.
        Vector2 centre = spawnPoint != null
            ? (Vector2)spawnPoint.position
            : (Vector2)transform.position;

        //generates a random position around the machine.
        Vector2 randomDirection = Random.insideUnitCircle.normalized;

        float randomDistance = Random.Range(minimumSpawnDistance,spawnRadius);

        Vector2 spawnPosition = centre + randomDirection * randomDistance;

        //creates the product at the random position.
        GameObject product = Instantiate(productPrefab,spawnPosition,Quaternion.identity);

        //records when a product is created.
        AnalyticsManager.SendCustomEvent(
            "ProductCreated",
            new Dictionary<string, object>
            {
                { "SpawnRadius", spawnRadius },
                { "ProductionCooldown", productionCooldown }     
            }
        );

        //tracks how many products were created
        SessionAnalytics.TrackProductCreated();

        //gets the product's Rigidbody2D.
        Rigidbody2D rb = product.GetComponent<Rigidbody2D>();

        //gives the product a push away from the machine.
        if (rb != null)
        {
            Vector2 pushDirection = (spawnPosition - centre).normalized;

            rb.AddForce(pushDirection * popForce,ForceMode2D.Impulse);
        }
    }

    //changes how quickly the machine produces products.
    public void UpgradeProductionSpeed(float reduction)
    {
        productionCooldown -= reduction;

        productionCooldown = Mathf.Round(productionCooldown * 10f) / 10f;

        productionCooldown = Mathf.Max(0.1f, productionCooldown);
    }
}
