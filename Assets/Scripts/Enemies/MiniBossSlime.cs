using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBossSlime : MonoBehaviour
{
    [Header("Game Elements")]
    MoveMaster moveM;

    [Header("Boss Slime Fields")]
    public bool miniRangedBoss;
    [SerializeField]
    int life = 100;
    int rangeDamage = 15;
    float rangeCooldown = 7;
    float rangeCounter = 1;
    float shootRatecounter;
    int meleeDamage = 20;
    float meleeCooldown = 2;
    float meleeCounter;
    float speed = 1.5f;
    Vector2 slimeBossPos;
    float detectionRadius = 14;
    bool isFacingRight;
    float idleTime;
    [SerializeField]
    GameObject spitPrefab;
    BossSpite spitScript;

    [Header("Player Fields")]
    [SerializeField]
    GameObject player;
    Vector2 playerPos;
    Player playerScript;

    [SerializeField]
    MiniBossSlimeState currentMiniBossSlimeState;
    enum MiniBossSlimeState
    {
        Idle,
        Attack,
        Dead
    }

    void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<Player>();
        spitScript = spitPrefab.GetComponent<BossSpite>();
    }

	void Update ()
    {
        playerPos = player.transform.position;

        switch (currentMiniBossSlimeState)
        {
            case MiniBossSlimeState.Idle:
                Idle();
                break;

            case MiniBossSlimeState.Attack:
                Attack();
                break;

            case MiniBossSlimeState.Dead:
                Dead();
                break;

            default:
                break;
        }		
	}

    #region UPDATE METHODS

    void Idle()
    {
        if(miniRangedBoss)
        {
            idleTime += Time.deltaTime;

            if (idleTime >= 4)
            {
                idleTime = 0;
                AttackState();
            }

        }
        else
        {
            if (Vector2.Distance(playerPos, slimeBossPos) < detectionRadius)
            {
                moveM.Move(this.gameObject, playerPos, speed);

                if (Vector2.Distance(playerPos, slimeBossPos) <= 1.5)
                {
                    AttackState();
                }
            }
        }
    }

    void Attack()
    {
        if(miniRangedBoss)
        {
            shootRatecounter += Time.deltaTime;
            idleTime += Time.deltaTime;

            if(shootRatecounter > 0.5f)
            {
                shootRatecounter = 0;
                spitScript.InitializateStats(1, rangeDamage, isFacingRight, true);
                Instantiate(spitPrefab, new Vector3(this.transform.position.x, this.transform.position.y, 0), new Quaternion(0, 0, 0, 0));
            }

            if(idleTime >= 3)
            {
                idleTime = 0;
                IdleState();
            }
        }
        else
        {
            meleeCounter -= Time.deltaTime;

            if (meleeCounter <= 0)
            {
                meleeCounter = meleeCooldown;  
                playerScript.RecieveDamage(meleeDamage);
            }

            if (Vector2.Distance(playerPos, slimeBossPos) > 1.5)
            {
                IdleState();
            }
        }

    }

    void Dead()
    {

    }

    #endregion

    #region STATE METHODS

    public void IdleState()
    {
        currentMiniBossSlimeState = MiniBossSlimeState.Idle;
    }

    public void AttackState()
    {
        currentMiniBossSlimeState = MiniBossSlimeState.Attack;
    }

    public void DeadState()
    {
        currentMiniBossSlimeState = MiniBossSlimeState.Dead;
    }

    #endregion

    void Flip()
    {
        this.gameObject.transform.localScale = new Vector3((this.gameObject.transform.localScale.x * -1), this.gameObject.transform.localScale.y, this.gameObject.transform.localScale.z);
        isFacingRight = !isFacingRight;
    }
    
    public void RecieveDamage(int damage)
    {
        life -= damage;

        if (life <= 0)
        {
            DeadState();
        }

    }
}
