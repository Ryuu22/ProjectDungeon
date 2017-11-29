using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    [Header("Slime Fields")]
    bool canAttack = false;
    int hitDamage = 3;
    float coolDownAttack = 1.0f;
    int life = 30;
    int fase = 3;
    float timeToLive;

    enum EnemyState { Idle, Patrol, Attack, Stun, Divide, Dead }
    [SerializeField] EnemyState currentState;

	void Update ()
    {
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

    }

    void Patrol()
    {

    }

    void Attack()
    {

    }

    void Stun()
    {

    }

    void Dead()
    {

    }

    #endregion

    #region STATE METHODS

    void IdleState()
    {
        currentState = EnemyState.Idle;
    }

    void PatrolState()
    {
        currentState = EnemyState.Patrol;
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
}
