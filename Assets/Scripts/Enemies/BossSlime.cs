using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSlime : MonoBehaviour
{
    [Header("Game Elements")]
    MoveMaster moveM;

    [Header("Boss Slime Fields")]
    [SerializeField]
    int life = 200;
    float speed = 0.8f;
    int meleeDamage = 25;
    float meleeCooldown = 2;
    float meleeCounter;
    int rangeDamage = 15;
    float rangeCooldown = 7;
    float rangeCounter = 1;
    Vector2 slimeBossPos;
    float detectionRadius = 14;
    bool isFacingRight;
    float idleTime;
    [SerializeField]
    GameObject spitPrefab;
    BossSpite spitScript;

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
        moveM = GameObject.FindGameObjectWithTag("MoveMaster").GetComponent<MoveMaster>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        spitScript = spitPrefab.GetComponent<BossSpite>();
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
        idleTime -= Time.deltaTime;

        if (Vector2.Distance(playerPos, slimeBossPos) < detectionRadius && idleTime <= 0)
        {
            ChasingState();
        }
    }

    void Chasing()
    {
        moveM.Move(this.gameObject, playerPos, speed);
        slimeBossPos = this.gameObject.transform.position;

        rangeCounter -= Time.deltaTime;

        if(rangeCounter <= 0)
        {
            rangeCounter = rangeCooldown;
            RangedAttackState();
        }

        if (Vector2.Distance(playerPos, slimeBossPos) > detectionRadius)
        {
            IdleState();
        }

        if (Vector2.Distance(playerPos, slimeBossPos) <= 4.5)
        {
            AttackState();
        }

        if (this.gameObject.transform.position.x - playerPos.x > 0 && isFacingRight)
        {
            Flip();
        }

        if (this.gameObject.transform.position.x - playerPos.x < 0 && !isFacingRight)
        {
            Flip();
        }
    }

    void Attack()
    {
        meleeCounter -= Time.deltaTime;

        if(meleeCounter <= 0)
        {
            meleeCounter = meleeCooldown;

            MeleeAttack();
        }

        if (Vector2.Distance(playerPos, slimeBossPos) > 4.5)
        {
            IdleState();
        }
    }

    void RangedAttack()
    {
        spitScript.InitializateStats(1, rangeDamage, isFacingRight);
        Instantiate(spitPrefab, new Vector3(this.transform.position.x, this.transform.position.y, 0), new Quaternion(0, 0, 0, 0));
        spitScript.InitializateStats(2, rangeDamage, isFacingRight);
        Instantiate(spitPrefab, new Vector3(this.transform.position.x, this.transform.position.y, 0), new Quaternion(0, 0, 0, 0));
        spitScript.InitializateStats(3, rangeDamage, isFacingRight);
        Instantiate(spitPrefab, new Vector3(this.transform.position.x, this.transform.position.y, 0), new Quaternion(0, 0, 0, 0));
        spitScript.InitializateStats(4, rangeDamage, isFacingRight);
        Instantiate(spitPrefab, new Vector3(this.transform.position.x, this.transform.position.y, 0), new Quaternion(0, 0, 0, 0));
        spitScript.InitializateStats(5, rangeDamage, isFacingRight);
        Instantiate(spitPrefab, new Vector3(this.transform.position.x, this.transform.position.y, 0), new Quaternion(0, 0, 0, 0));
        spitScript.InitializateStats(6, rangeDamage, isFacingRight);
        Instantiate(spitPrefab, new Vector3(this.transform.position.x, this.transform.position.y, 0), new Quaternion(0, 0, 0, 0));
        spitScript.InitializateStats(7, rangeDamage, isFacingRight);
        Instantiate(spitPrefab, new Vector3(this.transform.position.x, this.transform.position.y, 0), new Quaternion(0, 0, 0, 0));
        spitScript.InitializateStats(8, rangeDamage, isFacingRight);
        Instantiate(spitPrefab, new Vector3(this.transform.position.x, this.transform.position.y, 0), new Quaternion(0, 0, 0, 0));
        spitScript.InitializateStats(9, rangeDamage, isFacingRight);
        Instantiate(spitPrefab, new Vector3(this.transform.position.x, this.transform.position.y, 0), new Quaternion(0, 0, 0, 0));

        IdleState();
        idleTime = 2;
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

    void Flip()
    {
        this.gameObject.transform.localScale = new Vector3((this.gameObject.transform.localScale.x * -1), this.gameObject.transform.localScale.y, this.gameObject.transform.localScale.z);
        isFacingRight = !isFacingRight;
    }

    void MeleeAttack()
    {
        playerScript.RecieveDamage(meleeDamage);
    }

    public void RecieveDamage(int damage)
    {
        life -= damage;

        if (life <= 0)
        {
            DeadState();
        }

    }

    public int Life { get { return life; } }
}
