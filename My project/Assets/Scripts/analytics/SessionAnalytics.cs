
using UnityEngine;
using System.Collections.Generic;
using Abertay.Analytics;

[DefaultExecutionOrder(-100)]
public class SessionAnalytics : MonoBehaviour
{
    //keeps track of the session analytics object.
    private static SessionAnalytics instance;

    //tracks how long the player has been playing.
    private float sessionStartTime;

    //keeps track of what the player has done during the session.
    private int productsCreated = 0;
    private int productsCollected = 0;
    private int totalMoneyEarned = 0;
    private int upgradesPurchased = 0;
    private int conveyorsPlaced = 0;

    //checks whether the session has already ended.
    private bool sessionEnded = false;

    //makes sure there is only one session tracker.
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

        //keeps the tracker when changing scenes.
        DontDestroyOnLoad(gameObject);
    }

    //starts the session timer.
    void Start()
    {
        sessionStartTime = Time.realtimeSinceStartup;
    }

    //tracks when a product is created.
    public static void TrackProductCreated()
    {
        if (instance != null)
            instance.productsCreated++;
    }

    //tracks when a product is collected.
    public static void TrackProductCollected(int moneyEarned)
    {
        if (instance != null)
        {
            instance.productsCollected++;
            instance.totalMoneyEarned += moneyEarned;
        }
    }

    //tracks when an upgrade is purchased.
    public static void TrackUpgradePurchased()
    {
        if (instance != null)
            instance.upgradesPurchased++;
    }

    //tracks when a conveyor is placed.
    public static void TrackConveyorPlaced()
    {
        if (instance != null)
            instance.conveyorsPlaced++;
    }

    //sends the session summary when the game closes.
    void OnApplicationQuit()
    {
        if (sessionEnded)
            return;

        sessionEnded = true;

        //calculates how long the player has been playing.
        float sessionDuration =
            Time.realtimeSinceStartup - sessionStartTime;

        //sends all the session information to the analytics system.
        AnalyticsManager.SendCustomEvent(
            "SessionEnded",
            new Dictionary<string, object>
            {
            { "SessionDurationSeconds", Mathf.Round(sessionDuration * 10f) / 10f },
            { "ProductsCreated", productsCreated },
            { "ProductsCollected", productsCollected },
            { "TotalMoneyEarned", totalMoneyEarned },
            { "UpgradesPurchased", upgradesPurchased },
            { "ConveyorsPlaced", conveyorsPlaced }
            }
        );
    }
}
