using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthHandler : MonoBehaviour
{
    #region Player Health Variables
    public int playerHealthMax = 100;
    public int playerHealthMin = 0;
    public int playerHealth;
    public Text playerHealthText;
    #endregion

    #region Enemy health Variables
    public int enemyHealthMax = 100;
    public int enemyHealthMin = 0;
    public int enemyHealth;
    public Text enemyHealthText;
    #endregion

    #region Start and Update
    private void Start()
    {
        playerHealth = playerHealthMax;
        playerHealthText.text = ("Health : " + playerHealth).ToString();

        enemyHealth = enemyHealthMax;
        enemyHealthText.text = ("Health : " + enemyHealth).ToString();
    }

    private void Update()
    {
        LockHealthPlayer();
        LockHealthEnemy();   
    }
    #endregion

    #region Player Health Updates
    //This Function Locks the minimum and maximum Player health values
    public void LockHealthPlayer()
    {
        if (playerHealth <= playerHealthMin)
        {
            playerHealth = playerHealthMin;
            playerHealthText.text = ("Health : " + playerHealth);
        }
        if (playerHealth >= playerHealthMax)
        {
            playerHealth = playerHealthMax;
            playerHealthText.text = ("Health : " + playerHealth);
        }
    }
    
    // This function updates the players health text
    public void UpdatePlayerHealth(int damage)
    {
        { 
            playerHealth -= damage;
            playerHealthText.text = ("Health : " + playerHealth);
        }
    }

    // This function handled the player Healing
    public void UpdatePlayerHeal(int heal)
    {
        
        playerHealth += heal;
        playerHealthText.text = ("Health : " + playerHealth);

    }
    #endregion

    #region Enemy Health Updates
    //This Function Locks the minimum and maximum health values
    public void LockHealthEnemy()
    {
        if (enemyHealth <= enemyHealthMin)
        {
            enemyHealth = enemyHealthMin;
            enemyHealthText.text = ("Health : " + enemyHealth);
        }
        if (enemyHealth >= enemyHealthMax)
        {
            enemyHealth = enemyHealthMax;
            enemyHealthText.text = ("Health : " + enemyHealth);
        }
    }

    // This function updates the enemies health text
    public void UpdateEnemyHealth(int damage)
    {
      
        enemyHealth -= damage;
        enemyHealthText.text = ("Health : " + enemyHealth);

    }

    //This function handles the enemy healing
    public void UpdateEnemyHeal(int heal)
    {

        enemyHealth += heal;
        enemyHealthText.text = ("Health : " + enemyHealth);

    }
    #endregion


}
