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
    //State variable, and enum states for state machine.
    State state;
    public enum State
    {
        Confident,
        Wary,
        Panicked,
        Desperate,
        Dead,
        Wait
    }
    #endregion

    #region Start / Update (Inlcudes Statemachine Funcitons)
    private void Start()
    {
        healthHandler = FindObjectOfType<HealthHandler>();
        damageHandler = FindObjectOfType<DamageHandler>();
        EnemyHealthCheck();
        ChangeState();
    }

    private void Update()
    {
        // This function is a switch statement for the statemachine
       // EnemyHealthCheck();
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
        else if (healthHandler.enemyHealth >= 1)
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

    //This function takes a switch case and then runs the coroutines for each state.
    private void ChangeState()
    {
        Debug.Log(state.ToString());
        switch (state)
        {
            case State.Confident:
                    StartCoroutine(ConfidentState());
                break;

            case State.Wary:
                    StartCoroutine(WaryState());

                break;

            case State.Panicked:
                    StartCoroutine(PanickedState());
                break;

            case State.Desperate:
                    StartCoroutine(DesperateState());
                break;

            case State.Dead:
                    StartCoroutine(EnemyDeath());
                break;

            case State.Wait:
                    StartCoroutine(WaitState());
                break;

            default:
                break;
        }


    }


    #endregion

    #region Coroutines
    //State Coroutine
    IEnumerator ConfidentState()
    {
        EnemyHealthCheck();
        while (state == State.Confident)
        {
            if (!damageHandler.enemyTurn)  // Checks if not enemy turn.
            { 
                state = State.Wait; //Change state to wait
                continue; // Continue back up to the top of the while loop
            }

            yield return new WaitForSeconds(2); // Wait for 2 seconds to make it look like the enemy is thinking.
            damageHandler.EnemyBigHit(); // Call the function
            yield return new WaitForSeconds(1);
            damageHandler.enemyTurn = false;
            damageHandler.playerTurn = true;
        }
        ChangeState();
    }
    IEnumerator WaitState()
    {
        while (state == State.Wait)
        {
            if (damageHandler.enemyTurn) // If enemies turn, do health check to change state.
            {
                EnemyHealthCheck();
            }
            yield return null;
        }
        ChangeState();
    }

    IEnumerator WaryState()
    {
        EnemyHealthCheck();
        while (state == State.Wary)
        {
            if (!damageHandler.enemyTurn)
            {
                state = State.Wait;
                continue;
            }

            yield return new WaitForSeconds(2);
            damageHandler.EnemyHit();
            damageHandler.enemyTurn = false;
            yield return new WaitForSeconds(1);
            damageHandler.playerTurn = true;

        }
        ChangeState();
    }

    IEnumerator PanickedState()
    {
        EnemyHealthCheck();
        while (state == State.Panicked)
        {
            if (!damageHandler.enemyTurn)
            {
                state = State.Wait;
                continue;
            }
            yield return new WaitForSeconds(2);
            damageHandler.EnemyHeal();
            damageHandler.enemyTurn = false;
            yield return new WaitForSeconds(1);
            damageHandler.playerTurn = true;

        }
        ChangeState();
    }
    IEnumerator EnemyDeath() // Upon enemy death, it is rotated to 'lie down' and then the players turn is never triggered back to true, which ends the game.
    {
        damageHandler.enemyTurn = false;
        damageHandler.playerTurn = false;
        yield return new WaitForSeconds(1);
        damageHandler.enemyDeath.transform.Rotate(0, 0, 90);
        Debug.Log("Enemy has died!");
    }



    //For the last state, I wanted the enemy to randomly choose out of its move list.
    IEnumerator DesperateState()
    {
        EnemyHealthCheck();
        while (state == State.Desperate)
        {
            if (!damageHandler.enemyTurn)
            {
                state = State.Wait;
                continue;
            }
            float desperation = Random.Range(1, 4); // This randomly rolls a number between 1 and 4.
            yield return new WaitForSeconds(2); // Wait a moment for enemy to 'think'

            //Then actions one of the below functions according to the number rolled.
            if (desperation == 1)
            {
                damageHandler.EnemyBigHit();
                damageHandler.enemyTurn = false;
                damageHandler.playerTurn = true;

                Debug.Log("random is 1");
            }
            else if (desperation == 2)
            {
                damageHandler.EnemyHit();
                damageHandler.enemyTurn = false;
                damageHandler.playerTurn = true;

                Debug.Log("random is 2");
            }
            else if (desperation == 3)
            {
                damageHandler.EnemyShield();
                damageHandler.enemyTurn = false;
                damageHandler.playerTurn = true;

                Debug.Log("random is 3");
            }
            else if (desperation == 4)
            {
                damageHandler.EnemyHeal();
                damageHandler.enemyTurn = false;
                damageHandler.playerTurn = true;

                Debug.Log("random is 4");
            }

        }

        ChangeState();
    }


    #endregion

    #region Known Bugs

    //If Player spam clicks any action, and or doesnt wait a few seconds before making their decision the state machine reaction can be bypassed. and enemy can be killed down to nothing in moments. Unsure how to fix would love advice.

    #endregion
}
