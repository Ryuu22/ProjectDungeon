using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpitterSlime : MonoBehaviour
{
    [Header("Slime Fields")]
    [SerializeField]
    int life = 20;
    public float detectionRadius;
    Vector2 spitterSlimePos;
    float attackCooldown = 2;
    float attackCounter;
    [SerializeField]
    GameObject spitGameObject;
    Animator myAnim;

    //provisional
    float deadTime = 0.5f;
    float deadCounter;

    [Header("Player Fields")]
    Transform player;
    Vector2 playerPos;

    [SerializeField]
    SpitterSlimeState currentSpitterSlimeState;
    enum SpitterSlimeState
    {
        Idle,
        Attack,
        Dead,
    }

    void Start ()
    {
        myAnim = GetComponentInChildren<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
	}

	void Update ()
    {
        playerPos = player.position;
        spitterSlimePos = this.gameObject.transform.position;

        switch (currentSpitterSlimeState)
        {
            case SpitterSlimeState.Idle:
                Idle();
                break;

            case SpitterSlimeState.Attack:
                Attack();
                break;

            case SpitterSlimeState.Dead:
                Dead();
                break;

            default:
                break;
        }
    }

    #region UPDATE METHODS

    void Idle()
    {
        if (Vector2.Distance(playerPos, spitterSlimePos) < detectionRadius)
        {
            AttackState();
        }
    }

    void Attack()
    {
        if (attackCounter <= 0)
        {
            attackCounter = attackCooldown;
            spitGameObject.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 0);
            Instantiate(spitGameObject);
        }
        else attackCounter -= Time.deltaTime;
    }

    void Dead()
    {
        myAnim.SetTrigger("Dead");
        deadCounter -= Time.deltaTime;

        if(deadCounter <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    #endregion

    #region STATE METHODS

    public void IdleState()
    {
        currentSpitterSlimeState = SpitterSlimeState.Idle;
    }

    public void AttackState()
    {
        currentSpitterSlimeState = SpitterSlimeState.Attack;
    }

    public void DeadState()
    {
        currentSpitterSlimeState = SpitterSlimeState.Dead;
    }

    #endregion

    public void RecieveDamage(int damage)
    {
        life -= damage;

        if (life <= 0)
        {
            DeadState();
            deadCounter = deadTime;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(spitterSlimePos, detectionRadius);
    }

}
