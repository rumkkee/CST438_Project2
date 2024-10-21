using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This exists for saving
[CreateAssetMenu(fileName = "playerStatsObject", menuName = "Player Stats Object")]
public class PlayerStats : ScriptableObject
{
    // Exists for stats reasons
    public static long totalCurrency;
    // Again stats reasons
    public long totalStepsTaken;
    // How many steps can the player take before getting thrown back to the start?
    public int stepsAvaliable;
    // How many hits a player can take before zucking dying.
    public int health; 
    // How much is our starting health? 
    public int startingHealth;  
    // How much Armor does the player has? 
    public int armorValue;  
    // How much is our starting armor
    public int startingArmor;
    // Consumable list for storing consumables. 
    public int startingAttack = 1;
    public int startingSpeed = 1;
    public double startingCurrency = 0.00;
    public int startingStepsTaken = 0;
    public int startingStepsAvailable = 16;

    //=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-
    // Items list for storing items.
    public List<HealthItem> healthItems = new List<HealthItem>();
    public List<DamageItem> damageItems = new List<DamageItem>();
    public List<ArmorItem> armorItems = new List<ArmorItem>();
    public List<SpeedItem> speedItems = new List<SpeedItem>();

    public delegate void StepsUpdated(int steps);
    public static StepsUpdated StepsRemainingUpdated; // The number of steps in the current stack

    public delegate void CurrencyUpdated(double currency);
    public static CurrencyUpdated StartingCurrencyUpdated;

    public delegate void HealthUpdated(int health);
    public static HealthUpdated StartingHealthUpdated;
    public static HealthUpdated MaxHealthUpdated;

    public void Awake()
    {
        //health = 5;
        //startingHealth = 3;
    }

    public void SetMaxHealth(int maxHealth)
    {
        this.health = maxHealth;
        MaxHealthUpdated(maxHealth);
    }

    public void SetCurrentHealth(int currentHealth) { 
        startingHealth = currentHealth;
        StartingHealthUpdated(startingHealth);
    }

    public void UpdateCurrency(int changeInCurrency)
    {
        startingCurrency += changeInCurrency;
        StartingCurrencyUpdated(startingCurrency);
    }

    #region Steps
    public void stepTaken()
    {
        stepsTakenUpdated(++startingStepsTaken);
    }

    public void stepReversed()
    {
        stepsTakenUpdated(--startingStepsTaken);
    }

    public void stepsTakenUpdated(int stepsTaken)
    {
        
        startingStepsTaken = stepsTaken;
        StepsRemainingUpdated(remainingSteps());
    }

    public int remainingSteps()
    {
        return startingStepsAvailable - startingStepsTaken;
    }
    #endregion

}
