using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{

    #region Class References
    HealthHandler healthHandler;
    DamageHandler damageHandler;
    #endregion

    #region Enum
    State state;
    public enum State
    {
        Confident,
        Wary,
        Panicked,
        Desperate,
        Dead
    }
    #endregion

    #region Start / Update (Inlcudes Statemachine Funcitons)
    private void Start()
    {
        healthHandler = FindObjectOfType<HealthHandler>();
        damageHandler = FindObjectOfType<DamageHandler>();
    }

    private void Update()
    {
        // This function is a switch statement for the statemachine
        EnemyHealthCheck();

        switch (state)
        {
            case State.Confident:
                damageHandler.EnemyBigHit();
                break;

            case State.Wary:
                damageHandler.EnemyHit();
                break;

            case State.Panicked:
                damageHandler.EnemyHeal();
                break;

            case State.Desperate:
                //damageHandler.EnemyShield();
                damageHandler.EnemyHit();
                break;
            
            case State.Dead:
                damageHandler.EnemyDeath();
                break;

            default:
                break;
        }
    }
    #endregion

    #region StateMachine Checks
    //This function checks the enemies health and then changes the state in the state machine.
    public void EnemyHealthCheck()
    {
        if (healthHandler.enemyHealth >= 80)
        {
            state = State.Confident;
            Debug.Log("Enemy is Confident");
        }
        else if (healthHandler.enemyHealth >= 60)
        {
            state = State.Wary;
            Debug.Log("Enemy is Wary");
        }
        else if (healthHandler.enemyHealth >= 40)
        {
            state = State.Panicked;
            Debug.Log("Enemy is Panicked");
        }
        else if (healthHandler.enemyHealth >= 20)
        {
            state = State.Desperate;
            Debug.Log("Enemy is Desperate");
        }
        else if (healthHandler.enemyHealth <= 0)
        {
            state = State.Dead;
            Debug.Log("Enemy is Dead");
        }
    }
    #endregion
}
