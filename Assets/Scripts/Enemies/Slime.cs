using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    [Header("Game Elements")]
    MoveMaster moveM;
    AudioMaster audioM;

    [Header("Player Fields")]
    Transform player;
    Vector3 playerPos;
    Player playerScript;

    [Header("Slime Prefab")]
    [SerializeField] GameObject slimePrefab;
    [SerializeField] GameObject slimeMediumPrefab;
    [SerializeField] GameObject slimeSmallPrefab;

    [Header("Slime Fields")]
    [SerializeField]
    int phase;
    int life = 30;
    float detectionRadius = 4;
    float speed = 1;
    float attackCooldown;
    int damage = 3;
    Vector3 slimePos;
    float patrolMoveCounter;
    float velocityX;
    bool isFacingRight;
    float attackCounter;
    float stunnedTime = 0.7f;
    float stunnedCounter;
    float IdleCounter;
    float IdleTime;
    float deadTime = 0.5f;
    float randomScale;
    Animator myAnim;
    Rigidbody rb;

    [SerializeField]
    SlimeState currentSlimeState;
    enum SlimeState
    {
        Idle,
        Chasing,
        Dead
    }

    private void Start()
    {
        moveM = GameObject.FindGameObjectWithTag("MoveMaster").GetComponent<MoveMaster>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        myAnim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
        //audioM = GameObject.FindGameObjectWithTag("SoundMaster").GetComponent<AudioMaster>();
    }

    void Update ()
    {
        playerPos = player.transform.position;
        slimePos = this.transform.position;
        attackCooldown -= Time.deltaTime;

        switch(currentSlimeState)
        {
            case SlimeState.Idle:
                Idle();
                break;

            case SlimeState.Chasing:
                Chasing();
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
        if(Vector2.Distance(playerPos,slimePos) < detectionRadius)
        {
            ChasingState();
        }
    }

    void Chasing()
    {
        if(Vector2.Distance(playerPos, slimePos) > 2)
        {
            moveM.Move(this.gameObject, playerPos, speed);

            float posZ;
            posZ = this.transform.position.z;

            if (this.gameObject.transform.position.z < playerPos.z)
            {
                posZ += speed/2 * Time.deltaTime;
                this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, posZ);
            }
            if (this.gameObject.transform.position.z > playerPos.z)
            {
                posZ -= speed/2 * Time.deltaTime;
                this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, posZ);
            }
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
    
    void Dead()
    {
        deadTime -= Time.deltaTime;
        myAnim.SetTrigger("Dead");

        if (deadTime <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    #endregion

    #region STATE METHODS

    void IdleState()
    {
        currentSlimeState = SlimeState.Idle;
    }

    void ChasingState()
    {
        currentSlimeState = SlimeState.Chasing;
    }

    void DeadState()
    {
        currentSlimeState = SlimeState.Dead;
    }

    #endregion

    #region COLLISION METHODS

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == ("Player") && currentSlimeState == SlimeState.Chasing && attackCooldown <= 0)
        {
            myAnim.SetTrigger("Attack");
            playerScript.RecieveDamage();
            attackCooldown = 2;
        }
    }

    #endregion

    #region MECHANICS METHODS

    void Flip()
    {
        this.gameObject.transform.localScale = new Vector3((this.gameObject.transform.localScale.x * -1), this.gameObject.transform.localScale.y, this.gameObject.transform.localScale.z);
        isFacingRight = !isFacingRight;
    }

    public void RecieveDamage()
    {
        DeadState();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(slimePos, detectionRadius);       
    }

    #endregion

}
