using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    [Header("Game Elements")]
    MoveMaster moveM;

    [Header("Slime Fields")]
    [SerializeField] int life = 30;
    [SerializeField] public int phase = 3;
    [SerializeField] float speed = 1;
    Vector2 slimeStartPos;
    Vector2 slimePos;
    float velocityX;
    bool isFacingRight;
    Vector2 patrolTarget;
    float patrolMoveCounter;
    [SerializeField]
    float detectionRadius;
    int damage = 3;
    float attackCooldown = 2;
    float attackCounter;
    float stunnedTime = 0.7f;
    float stunnedCounter;
    float IdleCounter;
    float IdleTime;
    Animator myAnim;

    //provisional
    float deadTime = 0.5f;
    float deadCounter;


    Rigidbody2D rb;

    [Header("Player Fields")]
    Transform player;
    Vector2 playerPos;
    Player playerScript;

    [Header("Slime Prefab")]
    [SerializeField] GameObject slimePreFab;

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
        slimeStartPos = this.transform.position;
        myAnim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        moveM = GameObject.FindGameObjectWithTag("MoveMaster").GetComponent<MoveMaster>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
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
            ChasingState();
        }

        if(this.gameObject.transform.position.x - patrolTarget.x > 0 && isFacingRight)
        {
            isFacingRight = false;
            Flip();
        }
        if(this.gameObject.transform.position.x - patrolTarget.x < 0 && !isFacingRight)
        {
            isFacingRight = true;
            Flip();
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
            isFacingRight = false;
            Flip();
        }
        if(this.gameObject.transform.position.x - playerPos.x < 0 && !isFacingRight)
        {
            isFacingRight = true;
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
        IdleState();
        DivideAndDestroy(phase);
    }

    void Dead()
    {
        myAnim.SetTrigger("Dead");
        deadCounter -= Time.deltaTime;

        if (deadCounter <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    #endregion

    void Flip()
    {
        this.gameObject.transform.localScale = new Vector3((this.gameObject.transform.localScale.x * -1), this.gameObject.transform.localScale.y, this.gameObject.transform.localScale.z);
    }

    public void RecieveDamage(int damage)
    {
        life -= damage;


        if(life <= 0)
        {
            phase--;
            if(phase <= 0)
            {
                DeadState();
                deadCounter = deadTime;
            }
            else
            {
                DividingState();
            }
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

    private void DivideAndDestroy(int phase)
    {
        slimePreFab.GetComponent<Slime>().life = phase * 10;
        slimePreFab.GetComponent<Slime>().speed *= 2;
        slimePreFab.transform.localScale *= 0.8f;

        for(int i = 0; i < phase + 1;i++)Instantiate(slimePreFab);

  
        Debug.Log(this.gameObject.tag);
            
        Destroy(this.gameObject);
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(slimePos, detectionRadius);       
    }
}
