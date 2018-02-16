﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSlime : MonoBehaviour
{
    [Header("Game Elements")]
    MoveMaster moveM;
    [SerializeField]
    AudioSource audioSource;

    [Header("Player Fields")]
    Transform player;
    Vector2 playerPos;
    Player playerScript;

    [Header("Boss Slime Fields")]
    float speed = 0.8f;
    int lives = 3;
    public bool active;
    int timesAttacked;
    int phase;
    bool tired;
    float tiredCounter;
    bool recievedDamage;
    int meleeDamage = 25;
    float meleeCooldown = 2;
    float meleeCounter;

    int rangeDamage = 15;
    float rangeCooldown = 7;
    float rangeCounter = 1;

    float idleTime;
    Vector2 slimeBossPos;
    float detectionRadius = 11;
    bool isFacingRight;
    [SerializeField]
    GameObject spitPrefab;
    BossSpite spitScript;
    float deadTime = 0.5f;
    Animator myAnim;

    [SerializeField]
    BossSlimeState currentBossSlimeState;
    enum BossSlimeState
    {
        Inactive,
        Idle,
        Chasing,
        Attack,
        Tired,
        Dead
    }

	void Start ()
    {
        myAnim = GetComponentInChildren<Animator>();
        moveM = GameObject.FindGameObjectWithTag("MoveMaster").GetComponent<MoveMaster>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        //spitScript = spitPrefab.GetComponent<BossSpite>();
    }

	void Update ()
    {
        playerPos = player.transform.position;
        slimeBossPos = this.transform.position;

        switch (currentBossSlimeState)
        {
            case BossSlimeState.Inactive:
                if (active) IdleState(1);
                break;

            case BossSlimeState.Idle:
                Idle();
                break;

            case BossSlimeState.Chasing:
                Chasing();
                break;

            case BossSlimeState.Attack:
                Attack();
                break;

            case BossSlimeState.Tired:
                Tired();
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
        
        if(idleTime <= 0)
        {
            ChasingState();
        }
    }

    void Chasing()
    {
        if (Vector2.Distance(playerPos, slimeBossPos) > 3.5f)
        {
            moveM.Move(this.gameObject, playerPos, speed);

            float posZ;
            posZ = this.transform.position.z;

            if (this.gameObject.transform.position.z < player.position.z)
            {
                posZ += speed / 2 * Time.deltaTime;
                this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, posZ);
            }

            if (this.gameObject.transform.position.z > player.position.z)
            {
                posZ -= speed / 2 * Time.deltaTime;
                this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, posZ);
            }
        }
        else
        {
            myAnim.SetTrigger("Anticipation");
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
        if(phase == 0)
        {
            if(timesAttacked < 2)
            {
                myAnim.SetTrigger("Attack");
                timesAttacked++;
                IdleState(2);
            }
            else
            {
                myAnim.SetBool("Tired", true);
                tired = true;
                this.gameObject.GetComponent<BoxCollider>().isTrigger = true;
                timesAttacked = 0;
                tiredCounter = 8;
                TiredState();
            }
        }

        if (phase == 1)
        {
            if (timesAttacked < 3)
            {
                myAnim.SetTrigger("Attack");
                timesAttacked++;
                IdleState(2);
            }
            else
            {
                myAnim.SetBool("Tired", true);
                tired = true;
                this.gameObject.GetComponent<BoxCollider>().isTrigger = true;
                timesAttacked = 0;
                tiredCounter = 6;
                TiredState();
            }
        }

        if (phase == 2)
        {
            if (timesAttacked < 3)
            {
                myAnim.SetTrigger("Attack");
                timesAttacked++;
                IdleState(2);
            }
            else
            {
                myAnim.SetBool("Tired", true);
                tired = true;
                this.gameObject.GetComponent<BoxCollider>().isTrigger = true;
                timesAttacked = 0;
                tiredCounter = 5;
                TiredState();
            }
        }
    }

    void Tired()
    {
        if(tired)
        {
            tiredCounter -= Time.deltaTime;

            if(tiredCounter <= 0 && recievedDamage && lives == 1)
            {
                myAnim.SetTrigger("Dead");
                DeadState();
            }
            else if (tiredCounter <= 0 && recievedDamage)
            {
                myAnim.SetBool("Tired", false);
                this.gameObject.GetComponent<BoxCollider>().isTrigger = false;
                lives--;
                phase++;
                IdleState(1);
                tired = false;
            }

        }
    }

    void Dead()
    {

    }

    #endregion

    #region STATE METHDOS

    public void IdleState(int _idleTime)
    {
        currentBossSlimeState = BossSlimeState.Idle;
        idleTime = _idleTime;
    }

    public void ChasingState()
    {
        currentBossSlimeState = BossSlimeState.Chasing;
    }

    public void AttackState()
    {
        currentBossSlimeState = BossSlimeState.Attack;
    }

    public void TiredState()
    {
        currentBossSlimeState = BossSlimeState.Tired;
    }

    public void DeadState()
    {
        currentBossSlimeState = BossSlimeState.Dead;
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
        recievedDamage = true;
        this.gameObject.GetComponentInChildren<SpriteRenderer>().color = new Color(this.gameObject.GetComponentInChildren<SpriteRenderer>().color.r +100, this.gameObject.GetComponentInChildren<SpriteRenderer>().color.g, this.gameObject.GetComponentInChildren<SpriteRenderer>().color.a);
    }

    #endregion

}
