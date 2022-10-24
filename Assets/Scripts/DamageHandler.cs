using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageHandler : MonoBehaviour
{



    #region Player Damage & Heal Variables
    // These variables handle damage for the Player
    public int hitDamage = 30;
    public int bigHitDamage = 20;
    public int heal = 16;
    public bool activeShield = false;
    public bool playerTurn = true;
    public GameObject playerShield;
    #endregion

    #region Enemy Damage & Heal Variables
    // These variables handle the damage for the Enemy
    public int enemyHitDamage = 10;
    public int enemyBigHitDamage = 14;
    public int enemyHeal = 100;
    public bool enemyTurn = false;
    public bool enemyActiveShield = false;
    public GameObject enemyShield;
    public GameObject enemyDeath;
    #endregion

    #region References
    //Reference to the HealthHandler Class
    HealthHandler healthHandler;
    #endregion

    #region Start & Update
    private void Start()
    {
         
        healthHandler = FindObjectOfType<HealthHandler>();
        
        playerShield.SetActive(false);
        enemyShield.SetActive(false);

    }

    private void Update()
    {

        healthHandler.LockHealthPlayer();
    }

    #endregion

    #region Player Battle Functions

    // These Functions govern Player damage, these are attached to the canvas buttons.
    public void PlayerHit()
    {
        if (playerTurn && enemyActiveShield == false)
        {
            playerTurn = false;
            enemyTurn = true;
            healthHandler.UpdateEnemyHealth(hitDamage);
        }
        else if( playerTurn && enemyActiveShield == true)
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
        if (playerTurn && enemyActiveShield == false)
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

    public void PlayerHeal()
    {
        if (playerTurn)
        {
            healthHandler.UpdatePlayerHeal(heal);
            playerTurn = false;
            enemyTurn = true;
        }
        else
        {
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
        if (enemyTurn && activeShield == false)
        {
            enemyTurn = false;
            playerTurn = true;
            healthHandler.UpdatePlayerHealth(enemyHitDamage);
           
            Debug.Log("Player Hit");
        }
        else if(enemyTurn && activeShield == true) 
        {
            playerShield.SetActive(false);
            enemyTurn = false;
            playerTurn = true;
            activeShield = false;
            
            Debug.Log("PLAYER Shield Down!");
        }
    }


    public void EnemyBigHit()
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

    public void EnemyHeal()
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

    public void EnemyShield()
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

    public void EnemyDeath()
    {
        if (enemyTurn)
        {
            enemyDeath.transform.Rotate(0, 0, 90);
            Debug.Log("Enemy has died!");
        }
        else { return; }
    }


    #endregion
}
