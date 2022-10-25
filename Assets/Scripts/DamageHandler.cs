using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageHandler : MonoBehaviour
{



    #region Player Damage & Heal Variables
    // These variables handle damage for the Player
    public int hitDamage = 15;
    public int bigHitDamage = 20;
    public int heal = 15;
    public bool activeShield = false;
    public bool playerTurn = true; // To check if it is the players turn
    public GameObject playerShield; // Sprite for player shield
    #endregion

    #region Enemy Damage & Heal Variables
    // These variables handle the damage for the Enemy
    public int enemyHitDamage = 10;
    public int enemyBigHitDamage = 15;
    public int enemyHeal = 10;
    public bool enemyTurn = false; // To check if it is the enemys turn
    public bool enemyActiveShield = false;
    public GameObject enemyShield; // Sprite for enemy shield
    public GameObject enemyDeath; // Game Object to rotate enemy on death
    #endregion

    #region References
    //Reference to the HealthHandler Class
    HealthHandler healthHandler;
    #endregion

    #region Start & Update
    private void Start()
    {
         
        healthHandler = FindObjectOfType<HealthHandler>();
        
        playerShield.SetActive(false); // Setting playershield sprite as false on startup.
        enemyShield.SetActive(false); // Setting enemyshield sprite as false on startup.

    }

    private void Update()
    {

        healthHandler.LockHealthPlayer(); // Lock the players health at min/max
    }

    #endregion

    #region Player Battle Functions

    // These Functions govern Player damage, these are attached to the canvas buttons.
    public void PlayerHit()
    {
        if (playerTurn && enemyActiveShield == false) //Checks if its the players turn, and if the enemy does not have a shield.
        {
            playerTurn = false; // change player turn to false
            enemyTurn = true; // change enemy turn to true
            healthHandler.UpdateEnemyHealth(hitDamage); // Pass in the damage to the enemy
        }
        else if( playerTurn && enemyActiveShield == true) // Otherwise if the enemy does have a shield, as shield nullifies all damage, remove the shield, change the turn.
        {
            enemyShield.SetActive(false);
            enemyActiveShield = false;
            playerTurn = false;
            enemyTurn = true;

            Debug.Log("ENEMY Shield down");
        }
    }

    public void PlayerBigHit()
    {
        if (playerTurn && enemyActiveShield == false) // As above
        {
        healthHandler.UpdateEnemyHealth(bigHitDamage); 
        playerTurn = false;
        enemyTurn = true;
        }
        else if (playerTurn && enemyActiveShield == true)
        {
            enemyShield.SetActive(false);
            enemyActiveShield = false;
            playerTurn = false;
            enemyTurn = true;
            
            Debug.Log("ENEMY Shield down");
        }
    }
    //Handles the player healing
    public void PlayerHeal() 
    {
        if (playerTurn)
        {
            healthHandler.UpdatePlayerHeal(heal); // heals for the passed through amount.
            //Changes the turn
            playerTurn = false;
            enemyTurn = true;
        }
        else
        {
            //else if not the players turn do nothing.
            return;
        }
    }

    public void PlayerShield()
    {
        if (playerTurn)
        {
            activeShield = true;
            playerShield.SetActive(true);
            playerTurn = false;
            enemyTurn = true;
        }
        else { return; }
    }

    #endregion

    #region Enemy Battle Functions
    // These functions are automatically handled by the StateMachine Class
    public void EnemyHit()
    {
        if (enemyTurn && activeShield == false) // If it is the enemies turn, and the player does not have a shield
        {
            //Change the turns
            enemyTurn = false;
            playerTurn = true;
            healthHandler.UpdatePlayerHealth(enemyHitDamage); // Pass in the damage to the player

            Debug.Log("Player was Hit");
        }
        else if(enemyTurn && activeShield == true) // Otherwise if the player does have a shield, as shield nullifies all damage, remove the shield, change the turn.
        {
            playerShield.SetActive(false);
            enemyTurn = false;
            playerTurn = true;
            activeShield = false;
            
            Debug.Log("PLAYER Shield Down!");
        }
    }


    public void EnemyBigHit() //As above
    {
        if (enemyTurn && activeShield == false)
        {
            healthHandler.UpdatePlayerHealth(enemyBigHitDamage);
            enemyTurn = false;
            playerTurn = true;

            Debug.Log("Player was Hit BIGTIME");
        }
        else if (enemyTurn && activeShield == true)
        {
            playerShield.SetActive(false);
            enemyTurn = false;
            playerTurn = true;
            activeShield = false;

            Debug.Log("PLAYER Shield Down!");
        }
    }

    public void EnemyHeal() // Like the player healer, but for the enemy
    {
        if (enemyTurn)
        {
            healthHandler.UpdateEnemyHeal(enemyHeal);
            enemyTurn = false;
            playerTurn = true;

            Debug.Log("Enemy Healed!");
        }
        else
        {
            return;
        }
    }

    public void EnemyShield() // Like the player shield but for the enemy.
    {
        if (enemyTurn)
        {
            enemyActiveShield = true;
            enemyShield.SetActive(true);
            enemyTurn = false;
            playerTurn = true;

            Debug.Log("Enemy has Shielded!");
        }
        else { return; }
    }

    #endregion
}
