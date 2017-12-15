using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSlime : MonoBehaviour
{
    [Header("Game Elements")]
    MoveMaster mov;

    [Header("Boss Slime Fields")]
    [SerializeField]
    float life = 500;
    float speed = 5;
    int meleeDamage = 25;
    float meleeCooldown = 2;
    float meleeCounter;
    int rangeDamage = 15;
    float rangeCooldown = 10;
    float rangeCounter;
    bool isFacingRight;

    [Header("Player Fields")]
    Transform player;
    Vector2 playerPos;
    Player playerScript;

    [SerializeField]
    BossSlimeState currentBossSlimeState;
    enum BossSlimeState
    {
        Idle,
        Chasing,
        Attack,
        RangedAttack,
        Dead
    }

	void Start ()
    {
        mov = GameObject.FindGameObjectWithTag("MoveMaster").GetComponent<MoveMaster>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

	void Update ()
    {
        playerPos = player.transform.position;

        switch (currentBossSlimeState)
        {
            case BossSlimeState.Idle:
                Idle();
                break;

            case BossSlimeState.Chasing:
                Chasing();
                break;

            case BossSlimeState.Attack:
                Attack();
                break;

            case BossSlimeState.RangedAttack:
                RangedAttack();
                break;

            case BossSlimeState.Dead:
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

    void Chasing()
    {

    }

    void Attack()
    {

    }

    void RangedAttack()
    {

    }

    void Dead()
    {

    }

    #endregion

    #region STATE METHDOS

    public void IdleState()
    {
        currentBossSlimeState = BossSlimeState.Idle;
    }

    public void ChasingState()
    {
        currentBossSlimeState = BossSlimeState.Chasing;
    }

    public void AttackState()
    {
        currentBossSlimeState = BossSlimeState.Attack;
    }

    public void RangedAttackState()
    {
        currentBossSlimeState = BossSlimeState.RangedAttack;
    }

    public void DeadState()
    {
        currentBossSlimeState = BossSlimeState.Dead;
    }

    #endregion
}
