using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveSlime : MonoBehaviour
{
    [Header("Game Elements")]
    MoveMaster moveM;
    public GameObject[] essence;

    [Header("Slime Fields")]
    [SerializeField]
    int life = 20;
    Vector2 actualPos;
    float speed = 1;
    int damage = 5;
    float damageCooldown = 2;
    float damageCounter;
    float patrolCounter;
    float patrolMoveCounter;
    [SerializeField]
    Vector2 patrolTarget;
    Animator myAnim;
    float randomScale;
    float deadTime = 0.5f;

    [SerializeField]
    PassiveSlimeState currentPassiveSlimeState;
    enum PassiveSlimeState
    {
        Idle,
        TargetPatrol,
        Patrol,
        Dead
    }

    void Start ()
    {
        actualPos = this.transform.position;
        moveM = GameObject.FindGameObjectWithTag("MoveMaster").GetComponent<MoveMaster>();
        myAnim = GetComponentInChildren<Animator>();
        randomScale = Random.Range(0.8f, 1.2f);
        this.gameObject.transform.localScale = new Vector3(randomScale, randomScale, 1);
    }

	void Update ()
    {
        damageCounter -= Time.deltaTime;

        switch (currentPassiveSlimeState)
        {
            case PassiveSlimeState.Idle:
                Idle();
                break;

            case PassiveSlimeState.TargetPatrol:
                TargetPatrol();
                break;

            case PassiveSlimeState.Patrol:
                Patrol();
                break;

            case PassiveSlimeState.Dead:
                Dead();
                break;

            default:
                break;
        }
    }

    #region UPDATE METHODS

    void Idle()
    {
        patrolCounter -= Time.deltaTime;

        if(patrolCounter <= 0)
        {
            patrolCounter = Random.Range(4, 9);
            TargetPatrolState();
        }
    }

    void TargetPatrol()
    {
        patrolTarget = new Vector2(actualPos.x + Random.Range(-3, 3), actualPos.y + Random.Range(-2, 2));
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
    }

    void Dead()
    {
        myAnim.SetTrigger("Dead");
        deadTime -= Time.deltaTime;

        if (deadTime <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    #endregion

    #region STATE METHODS

    void IdleState()
    {
        currentPassiveSlimeState = PassiveSlimeState.Idle;
    }

    void TargetPatrolState()
    {
        currentPassiveSlimeState = PassiveSlimeState.TargetPatrol;
    }

    void PatrolState()
    {
        currentPassiveSlimeState = PassiveSlimeState.Patrol;
    }

    void DeadState()
    {
        DropEssences();
        currentPassiveSlimeState = PassiveSlimeState.Dead;
    }

    #endregion

    #region MECHANICS METHODS

    public void RecieveDamage(int damage)
    {
        life -= damage;
        Debug.Log("AH");
        if (life <= 0)
        {
            DeadState();
        }
    }

    void Damage(GameObject player)
    {
        if(damageCounter <= 0)
        {
            damageCounter = damageCooldown;
            player.GetComponent<Player>().RecieveDamage(damage);
        }
    }

    void DropEssences()
    {
        for (int i = 0; i < essence.Length; i++)
        {
            essence[i].SetActive(true); //step 1: Activation
            essence[i].GetComponent<AudioSource>().Play();
            essence[i].GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.value ,Random.value),ForceMode2D.Impulse); //step 2: Random force
            essence[i].transform.parent = null; //step 3: Unparenting
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Damage(other.gameObject);
        }
    }

    #endregion

}
