using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Game Elements")]
    InputMaster inputM;
    CollisionMaster collisionM;

    [Header("Player Fields")]
    [SerializeField]
    int life = 100;
    [SerializeField]
    int maxLife = 100;
    bool isDead;
    Vector2 movementSpeed = Vector2.zero;
    Vector2 speed = new Vector2(5, 5);

    Vector2 dashDirection;
    float dashCooldown = 5;
    float dashCooldownCounter;
    float dashSpeed = 15;
    float dashTime = 0.1f;
    float dashCounter;
    bool isDashing;

    float attackCooldown = 1;
    float attackCounter;
    int damage = 10;
    float arrowCooldown = 1.5f;
    float arrowCounter;
    Animator myAnim;
    public GameObject arrowGameObject;

    public Vector2 attackBoxPos;
    public Vector2 attackBoxSize;
    public ContactFilter2D filter;

    void Start ()
    {
        inputM = GameObject.FindGameObjectWithTag("InputMaster").GetComponent<InputMaster>();
        collisionM = GetComponent<CollisionMaster>();
        myAnim = GetComponentInChildren<Animator>();
    }

	void FixedUpdate ()
    {
        Movement();
    }

    private void Update()
    {
        if(attackCooldown > 0)
        {
            attackCounter -= Time.deltaTime;
        }
        if(arrowCounter > 0)
        {
            arrowCounter -= Time.deltaTime;
        }
        if (dashCooldownCounter > 0)
        {
            dashCooldownCounter -= Time.deltaTime;
        }
    }

    void Movement()
    {
        if(!isDead)
        {
            if (isDashing)
            {
                Dash(dashDirection);
            }
            else
            {
                Vector3 provisionalPos;

                provisionalPos = this.transform.position;
                movementSpeed = speed;

                if(inputM.GetAxis().x > 0.1 || inputM.GetAxis().y > 0.1 || inputM.GetAxis().x < -0.1 || inputM.GetAxis().y < -0.1)
                {
                    myAnim.SetBool("Moving", true);
                }
                if (inputM.GetAxis().x < 0.1 && inputM.GetAxis().x > -0.1 && inputM.GetAxis().y < 0.1 && inputM.GetAxis().y > -0.1)
                {
                    myAnim.SetBool("Moving", false);
                }

                if (collisionM.IsLeftWalled && inputM.GetAxis().x < 0)
                {
                    movementSpeed.x = 0;
                    myAnim.SetBool("Moving", false);
                }

                if (collisionM.IsRightWalled && inputM.GetAxis().x > 0)
                {
                    movementSpeed.x = 0;
                    myAnim.SetBool("Moving", false);
                }

                if (collisionM.IsTopWalled && inputM.GetAxis().y > 0)
                {
                    movementSpeed.y = 0;
                    myAnim.SetBool("Moving", false);
                }

                if (collisionM.IsBottomWalled && inputM.GetAxis().y < 0)
                {
                    movementSpeed.y = 0;
                    myAnim.SetBool("Moving", false);
                }

                provisionalPos.x += inputM.GetAxis().x * Time.deltaTime * movementSpeed.x;
                provisionalPos.y += inputM.GetAxis().y * Time.deltaTime * movementSpeed.y;

                this.transform.position = provisionalPos;

                if(inputM.GetAxis().x > 0.1)
                {
                    this.gameObject.transform.localScale = new Vector3(1, 1, 1);
                    attackBoxPos.x = 0.75f;
                }
                if (inputM.GetAxis().x < -0.1)
                {
                    this.gameObject.transform.localScale = new Vector3(-1, 1, 1);
                    attackBoxPos.x = -0.75f;
                }
            }
        }        
    }

    public void Attack()
    {
        if (attackCounter <= 0)
        {
            attackCounter = attackCooldown;
            myAnim.SetTrigger("Attack");

            Vector3 pos = this.transform.position + (Vector3)attackBoxPos;
            Collider2D[] results = new Collider2D[5];

            int numColliders = Physics2D.OverlapBox(pos, attackBoxSize, 0, filter, results);

            for (int i = 0; i < numColliders; i++)
            {
                if (results[i].gameObject.tag == "Slime")
                {
                    Damage(results[i].gameObject, "Slime");
                }
                if (results[i].gameObject.tag == "SpitterSlime")
                {
                    Damage(results[i].gameObject, "SpitterSlime");
                }
                if (results[i].gameObject.tag == "PassiveSlime")
                {
                    Damage(results[i].gameObject, "PassiveSlime");
                }
                if (results[i].gameObject.tag == "Destructible")
                {
                    Damage(results[i].gameObject, "Destructible");
                }
            }
        }
    }

    public void Shoot()
    {
        if(arrowCounter <= 0)
        {
            arrowCounter = arrowCooldown;
            arrowGameObject.transform.position = new Vector3(this.transform.position.x + attackBoxPos.x, this.transform.position.y + 1, 0);
            Instantiate(arrowGameObject);
        }
    }

    void Damage (GameObject target, string targetType)
    {
        if(targetType == ("Slime"))
        {
            target.GetComponent<Slime>().RecieveDamage(damage);
        }
        if (targetType == ("SpitterSlime"))
        {
            target.GetComponent<SpitterSlime>().RecieveDamage(damage);
        }
        if (targetType == ("PassiveSlime"))
        {
            target.GetComponent<PassiveSlime>().RecieveDamage(damage);
        }
        if (targetType == ("Destructible"))
        {
            target.GetComponent<DestructibleBehaviour>().GetDestroyed();
        }
    }

    public void BeginDash()
    {
        if(dashCooldownCounter <= 0)
        {
            dashCooldownCounter = dashCooldown;

            isDashing = true;
            myAnim.SetTrigger("Dash");

            if (inputM.GetAxis().x > 0.1) dashDirection.x = 1;
            if (inputM.GetAxis().x < -0.1) dashDirection.x = -1;
            if (inputM.GetAxis().x > -0.1 && inputM.GetAxis().x < 0.1) dashDirection.x = 0;

            if (inputM.GetAxis().y > 0.1) dashDirection.y = 1;
            if (inputM.GetAxis().y < -0.1) dashDirection.y = -1;
            if (inputM.GetAxis().y > -0.1 && inputM.GetAxis().y < 0.1) dashDirection.y = 0;
        }
    }

    void Dash(Vector2 direction)
    {
        Vector3 provisionalPos;
        provisionalPos = this.transform.position;
        dashCounter += Time.deltaTime;

        provisionalPos.x += direction.x * Time.deltaTime * dashSpeed;
        provisionalPos.y += direction.y * Time.deltaTime * dashSpeed;

        this.transform.position = provisionalPos;

        if (dashCounter >= dashTime)
        {
            dashCounter = 0;
            isDashing = false;
        }
    }

    public void RecieveDamage(int damage)
    {
        life -= damage;

        if (life <= 0)
        {
            isDead = true;
        }
    }
    public void RecieveHP(int HP)
    {
        life += HP;

        if (life >= maxLife)
        {
            life = maxLife;
        }
    }


    public int Life { get { return life; } }
    public bool IsDead { get { return isDead; } }
    public float DashCooldownCounter { get { return dashCooldownCounter; } }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Vector3 pos = this.transform.position + (Vector3)attackBoxPos;
        Gizmos.DrawWireCube(pos, attackBoxSize);
    }
}
