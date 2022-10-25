using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthHandler : MonoBehaviour
{
    #region Player Health Variables
    public int playerHealthMax = 100; // Max player Health
    public int playerHealthMin = 0; // Minimum player health
    public int playerHealth; // variable to show player health
    public Text playerHealthText; // Text to display player health
    #endregion

    #region Enemy health Variables
    public int enemyHealthMax = 100; // Max enemy Health
    public int enemyHealthMin = 0; // Minimum enemy health
    public int enemyHealth; // variable to show enemy health
    public Text enemyHealthText; // Text to display enemy health
    #endregion

    #region Start and Update
    private void Start()
    {
        playerHealth = playerHealthMax; //Setting the variable to = Max player health limit
        playerHealthText.text = ("Health : " + playerHealth).ToString(); // Converting the current player health to a string to be displayed.

        enemyHealth = enemyHealthMax; //Setting the variable to = Max enemy health limit
        enemyHealthText.text = ("Health : " + enemyHealth).ToString(); // Converting the current enemy health to a string to be displayed.
    } 

    private void Update()
    {
        LockHealthPlayer(); // Forcing the limits 0 and 100 for min max health for player
        LockHealthEnemy(); // Forcing the limits 0 and 100 for min max health for enemy
    }
    #endregion

    #region Player Health Updates
    //This Function Locks the minimum and maximum Player health values
    public void LockHealthPlayer()
    {
        if (playerHealth <= playerHealthMin) // If player health drops below 0
        {
            playerHealth = playerHealthMin; // Lock player health at zero
            playerHealthText.text = ("Health : " + playerHealth); // Update / Display player health on screen
        }
        if (playerHealth >= playerHealthMax) // If player health goes above 100
        {
            playerHealth = playerHealthMax; // Lock player health at 100
            playerHealthText.text = ("Health : " + playerHealth); // Update / Display player health on screen
        }
    }
    
    // This function updates the players health text
    public void UpdatePlayerHealth(int damage) // Change / update players health when damaged using the information passed in.
    {
        { 
            playerHealth -= damage; // Change player health according to damage inflicted
            playerHealthText.text = ("Health : " + playerHealth); // Update / Display new player health after damage
        }
    }

    // This function handled the player Healing
    public void UpdatePlayerHeal(int heal) // Change / update players health when healed using the information passed in.
    {
        
        playerHealth += heal; // Change player health according to heal variable
        playerHealthText.text = ("Health : " + playerHealth); // Update / Display new player health after healing

    }
    #endregion

    #region Enemy Health Updates
    //This Function Locks the minimum and maximum health values
    public void LockHealthEnemy()
    {
        if (enemyHealth <= enemyHealthMin) // If enemy health drops below 0
        {
            enemyHealth = enemyHealthMin; // Lock enemy health at zero
            enemyHealthText.text = ("Health : " + enemyHealth); // Update / Display enemy health on screen
        }
        if (enemyHealth >= enemyHealthMax) // If enemy health goes above 100
        {
            enemyHealth = enemyHealthMax; // Lock enemy health at 100
            enemyHealthText.text = ("Health : " + enemyHealth); // Update / Display enemy health on screen
        }
    }

    // This function updates the enemies health text
    public void UpdateEnemyHealth(int damage) // Change / update enemy health when healed using the information passed in.
    {
      
        enemyHealth -= damage; // Change enemy health according to damage inflicted
        enemyHealthText.text = ("Health : " + enemyHealth); // Update / Display enemy health on screen

    }

    //This function handles the enemy healing
    public void UpdateEnemyHeal(int heal) // Change / update enemys health when healed using the information passed in.
    {

        enemyHealth += heal; // Change enemys health according to heal variable
        enemyHealthText.text = ("Health : " + enemyHealth); // Update / Display new enemy health after healing

    }
    #endregion

    #region Known Bugs

    //If Player spam clicks any action, and or doesnt wait a few seconds before making their decision the state machine reaction can be bypassed. and enemy can be killed down to nothing in moments. Unsure how to fix would love advice.

    #endregion
}
