using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    [Header("Slime Fields")]
    [SerializeField] bool canAttack = false;
    [SerializeField] int hitDamage = 3;
    [SerializeField] float coolDownAttack = 1.0f;
    [SerializeField] int life = 30;
    [SerializeField] int fase = 3;
    [SerializeField] float timeToLive;

    [SerializeField] float IdleCounter;
    [SerializeField] float IdleTime; 



    public Transform player;
    Vector2 playerPosition;
    Vector2 position;

    public float detectionRadius;

    enum EnemyState { Idle, Patrol, Attack, Stun, Divide, Dead }
    [SerializeField] EnemyState currentState;

	void Update ()
    {
        playerPosition = player.transform.position;
        position = this.transform.position;
        switch(currentState)
        {
            case EnemyState.Idle:
                Idle();
                break;
            case EnemyState.Patrol:
                Patrol();
                break;
            case EnemyState.Attack:
                Attack();
                break;
            case EnemyState.Stun:
                Stun();
                break;
            case EnemyState.Dead:
                Dead();
                break;
            default:
                break;
        }
    }

    #region UPDATE METHODS

    void Idle()
    {
        IdleCounter += Time.deltaTime;

        if(IdleCounter >= IdleTime)
        {
            PatrolState();
        }

        //Checks distance between player and enemy
        if(Vector2.Distance(playerPosition,position) < detectionRadius)
        {
            AttackState();
        }

        //Checks death 
        if(life <= 0)
        {
            fase--;
            if(fase <= 0)
            {
                DeadState();
            }
            else
            {
                Divide(fase);
            }
        }

    }

    void Patrol()
    {
        IdleCounter += Time.deltaTime;

        if(IdleCounter >= IdleTime)
        {
            PatrolState();
        }

        if(Vector2.Distance(playerPosition, position) < detectionRadius)
        {
            currentState = EnemyState.Attack;
        }

    }

    void Attack()
    {
        if(Vector2.Distance(playerPosition, position) > detectionRadius)
        {
            currentState = EnemyState.Idle;
        }


    }

    void Stun()
    {

    }

    void Dead()
    {

    }

    #endregion

    void ReceiveDamage()
    {

    }

    void Divide(int fase)
    {

    }

    #region STATE METHODS

    void IdleState()
    {
        currentState = EnemyState.Idle;

        IdleCounter = 0.0f;
    }

    void PatrolState()
    {
        currentState = EnemyState.Patrol;

        IdleCounter = 0.0f;
    }

    void AttackState()
    {
        currentState = EnemyState.Attack;
    }

    void StunState()
    {
        currentState = EnemyState.Stun;
    }

    void DeadState()
    {
        currentState = EnemyState.Dead;
    }

    #endregion
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(position, detectionRadius);
       
    }
}
