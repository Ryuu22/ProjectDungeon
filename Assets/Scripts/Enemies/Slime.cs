using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    [Header("Game Elements")]
    MoveMaster moveM;

    [Header("Slime Fields")]
    [SerializeField]
    int phase;
    int life = 30;
    float detectionRadius;
    float speed = 1;
    float attackCooldown = 2;
    int damage = 3;

    Vector2 slimeStartPos;
    Vector2 slimePos;
    Vector2 patrolTarget;
    float patrolMoveCounter;
    float velocityX;
    bool isFacingRight;
    float attackCounter;
    float stunnedTime = 0.7f;
    float stunnedCounter;
    float IdleCounter;
    float IdleTime;
    float deadTime = 0.5f;
    float deadCounter;
    float randomScale;
    Animator myAnim;
    Rigidbody2D rb;

    [Header("Player Fields")]
    Transform player;
    Vector2 playerPos;
    Player playerScript;

    [Header("Slime Prefab")]
    [SerializeField] GameObject slimePrefab;
    [SerializeField] GameObject slimeMediumPrefab;
    [SerializeField] GameObject slimeSmallPrefab;

    [SerializeField]
    SlimeState currentSlimeState;
    enum SlimeState
    {
        Idle,
        TargetPatrol,
        Patrol,
        Chasing,
        Attack,
        Stunned,
        Dividing,
        Dead
    }

    private void Start()
    {
        InitializateStats();
        RandomizeScale();
        this.transform.localScale = new Vector3(randomScale, randomScale, 0);
        moveM = GameObject.FindGameObjectWithTag("MoveMaster").GetComponent<MoveMaster>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        myAnim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update ()
    {
        playerPos = player.transform.position;
        slimePos = this.transform.position;

        switch(currentSlimeState)
        {
            case SlimeState.Idle:
                Idle();
                break;

            case SlimeState.TargetPatrol:
                TargetPatrol();
                break;

            case SlimeState.Patrol:
                Patrol();
                break;

            case SlimeState.Chasing:
                Chasing();
                break;

            case SlimeState.Attack:
                Attack();
                break;

            case SlimeState.Stunned:
                Stunned();
                break;

            case SlimeState.Dividing:
                Dividing();
                break;

            case SlimeState.Dead:
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
            IdleCounter = 0;
            IdleTime = Random.Range(4, 9);
            TargetPatrolState();
        }

        if(Vector2.Distance(playerPos,slimePos) < detectionRadius)
        {
            ChasingState();
        }
    }

    void TargetPatrol()
    {
        patrolTarget = new Vector2(slimeStartPos.x + Random.Range(-3, 3), slimeStartPos.y + Random.Range(-2, 2));

        if(this.gameObject.transform.position.x - patrolTarget.x > 0 && isFacingRight)
        {
            Flip();
        }
        if(this.gameObject.transform.position.x - patrolTarget.x < 0 && !isFacingRight)
        {
            Flip();
        }

        PatrolState();
    }

    void Patrol()
    {
        patrolMoveCounter += Time.deltaTime;
        moveM.Move(this.gameObject, patrolTarget, speed);

        if(Vector2.Distance(this.transform.position, patrolTarget) < 1 || patrolMoveCounter > 5)
        {
            patrolMoveCounter = 0;
            IdleState();
        }

        if(Vector2.Distance(playerPos, slimePos) < detectionRadius)
        {
            if(this.gameObject.transform.position.x - playerPos.x > 0 && isFacingRight)
            {
                Flip();
            }
            if(this.gameObject.transform.position.x - playerPos.x < 0 && !isFacingRight)
            {
                Flip();
            }

            ChasingState();
        }
    }

    void Chasing()
    {
        moveM.Move(this.gameObject, playerPos, speed);

        if(Vector2.Distance(playerPos, slimePos) > detectionRadius)
        {
            IdleState();
        }

        if(this.gameObject.transform.position.x - playerPos.x > 0 && isFacingRight)
        {
            Flip();
        }
        if(this.gameObject.transform.position.x - playerPos.x < 0 && !isFacingRight)
        {
            Flip();
        }

    }

    void Attack()
    {
        attackCounter -= Time.deltaTime;

        if(attackCounter <= 0)
        {
            attackCounter = attackCooldown;

            playerScript.RecieveDamage(damage);
        }
    }

    void Stunned()
    {
        stunnedCounter -= Time.deltaTime;

        if (stunnedCounter <= 0)
        {
            stunnedCounter = stunnedTime;
            IdleState();
        }
    }

    void Dividing()
    {
        Divide();
        DeadState();
    }

    void Dead()
    {
        deadCounter -= Time.deltaTime;
        myAnim.SetTrigger("Dead");

        if (deadCounter <= 0)
        {
            deadCounter = deadTime;
            Divide();
            Destroy(this.gameObject);
        }
    }

    #endregion

    #region START METHODS

    void RandomizeScale()
    {
        if(phase == 3)
        {
            randomScale = Random.Range(0.9f, 1.2f);
        }
        if(phase == 2)
        {
            randomScale = Random.Range(0.5f, 0.8f);
        }
        if(phase == 1)
        {
            randomScale = Random.Range(0.2f, 0.3f);
        }
    }

    void InitializateStats()
    {
        slimeStartPos = this.transform.position;
        if(phase == 3)
        {
            life = 30;
            detectionRadius = 7;
            speed = 0.7f;
            attackCooldown = 2;
            damage = 20;
}
        if(phase == 2)
        {
            life = 15;
            detectionRadius = 5;
            speed = 1;
            attackCooldown = 1.5f;
            damage = 10;
        }
        if(phase == 1)
        {
            life = 5;
            detectionRadius = 3.5f;
            speed = 1.2f;
            attackCooldown = 1;
            damage = 3;
        }
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

        if(life <= 0)
        {
            deadCounter = deadTime;
            DeadState();
        }

        else
        {
            Vector2 oppositePosition;
            oppositePosition.x = slimePos.x - playerPos.x;
            oppositePosition.y = slimePos.y - playerPos.y;

            rb.AddForce(oppositePosition.normalized*3, ForceMode2D.Impulse);

            StunnedState();
        }
    }

    private void Divide()
    {
        if(phase == 3)
        {
            Instantiate(slimeMediumPrefab, new Vector3(slimePos.x, slimePos.y + 0.5f, 0), new Quaternion(0, 0, 0, 0));
            Instantiate(slimeMediumPrefab, new Vector3(slimePos.x, slimePos.y - 0.5f, 0), new Quaternion(0, 0, 0, 0));
        }
        if(phase == 2)
        {
            Instantiate(slimeSmallPrefab, new Vector3(slimePos.x, slimePos.y + 0.1f, 0), new Quaternion(0, 0, 0, 0));
            Instantiate(slimeSmallPrefab, new Vector3(slimePos.x, slimePos.y - 0.1f, 0), new Quaternion(0, 0, 0, 0));
        }
    }

    #region STATE METHODS

    void IdleState()
    {
        currentSlimeState = SlimeState.Idle;
    }

    void TargetPatrolState()
    {
        currentSlimeState = SlimeState.TargetPatrol;
    }

    void PatrolState()
    {
        currentSlimeState = SlimeState.Patrol;
    }

    void ChasingState()
    {
        currentSlimeState = SlimeState.Chasing;
    }

    void AttackState()
    {
        currentSlimeState = SlimeState.Attack;
    }

    void StunnedState()
    {
        currentSlimeState = SlimeState.Stunned;
    }

    void DividingState()
    {
        currentSlimeState = SlimeState.Dividing;
    }

    void DeadState()
    {
        currentSlimeState = SlimeState.Dead;
    }

    #endregion

    #region COLLISION METHODS

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == ("Player") && currentSlimeState == SlimeState.Chasing)
        {
            AttackState();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == ("Player") && currentSlimeState == SlimeState.Attack)
        {
            ChasingState();
        }
    }

    #endregion

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(slimePos, detectionRadius);       
    }
}
