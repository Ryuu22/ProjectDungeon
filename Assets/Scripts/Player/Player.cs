using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Game Elements")]
    InputMaster inputM;
    CollisionMaster collisionM;

    [Header("Player Fields")]
    Vector2 movementSpeed = Vector2.zero;
    Vector2 speed = new Vector2(5, 5);

    Vector2 dashDirection;
    float dashSpeed = 15;
    float dashTime = 0.2f;
    float dashCounter;
    bool isDashing;

    float AttackCooldown = 0.7f;
    float AttackCounter;
    int damage = 10;

    public GameObject arrowGameObject;

    public Vector2 attackBoxPos;
    public Vector2 attackBoxSize;
    public ContactFilter2D filter;

    void Start ()
    {
        inputM = GameObject.FindGameObjectWithTag("InputMaster").GetComponent<InputMaster>();
        collisionM = GetComponent<CollisionMaster>();
    }

	void FixedUpdate ()
    {
        Movement();
    }

    private void Update()
    {
        if(AttackCounter > 0)
        {
            AttackCounter -= Time.deltaTime;
        }    
    }

    void Movement()
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

            if (collisionM.IsLeftWalled && inputM.GetAxis().x < 0) movementSpeed.x = 0;

            if (collisionM.IsRightWalled && inputM.GetAxis().x > 0) movementSpeed.x = 0;

            if (collisionM.IsTopWalled && inputM.GetAxis().y > 0) movementSpeed.y = 0;

            if (collisionM.IsBottomWalled && inputM.GetAxis().y < 0) movementSpeed.y = 0;

            provisionalPos.x += inputM.GetAxis().x * Time.deltaTime * movementSpeed.x;
            provisionalPos.y += inputM.GetAxis().y * Time.deltaTime * movementSpeed.y;

            this.transform.position = provisionalPos;
        }
    }

    public void Attack()
    {
        if (AttackCounter <= 0)
        {
            AttackCounter = AttackCooldown;

            Vector3 pos = this.transform.position + (Vector3)attackBoxPos;
            Collider2D[] results = new Collider2D[5];

            int numColliders = Physics2D.OverlapBox(pos, attackBoxSize, 0, filter, results);

            for (int i = 0; i < numColliders; i++)
            {
                if (results[i].gameObject.tag == "Slime")
                {
                    Damage(results[i].gameObject, "Slime");
                }
            }
        }
    }

    public void Shoot()
    {
        Instantiate(arrowGameObject);
        arrowGameObject.transform.position = new Vector3(this.transform.position.x + 1, this.transform.position.y + 1, 0);
    }

    void Damage (GameObject target, string targetType)
    {
        if(targetType == ("Slime"))
        {
            target.GetComponent<Slime>().RecieveDamage(damage);
        }
    }

    public void BeginDash()
    {
        isDashing = true;
        dashDirection = new Vector2(inputM.GetAxis().x, inputM.GetAxis().y);
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

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Vector3 pos = this.transform.position + (Vector3)attackBoxPos;
        Gizmos.DrawWireCube(pos, attackBoxSize);
    }
}
