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
    int damage = 30;

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

    void Movement()
    {
        Vector3 provisionalPos;

        provisionalPos = this.transform.position;
        movementSpeed = speed;

        if(collisionM.IsLeftWalled && inputM.GetAxis().x < 0) movementSpeed.x = 0;

        if(collisionM.IsRightWalled && inputM.GetAxis().x > 0) movementSpeed.x = 0;

        if(collisionM.IsTopWalled && inputM.GetAxis().y > 0) movementSpeed.y = 0;

        if(collisionM.IsBottomWalled && inputM.GetAxis().y < 0) movementSpeed.y = 0;

        provisionalPos.x += inputM.GetAxis().x * Time.deltaTime * movementSpeed.x;
        provisionalPos.y += inputM.GetAxis().y * Time.deltaTime * movementSpeed.y;

        this.transform.position = provisionalPos;
    }

    public void Attack()
    {
        Vector3 pos = this.transform.position + (Vector3)attackBoxPos;
        Collider2D[] results = new Collider2D[5];

        int numColliders = Physics2D.OverlapBox(pos, attackBoxSize, 0, filter, results);

        for(int i = 0; i < numColliders; i++)
        {
            if(results[i].gameObject.tag == "Slime")
            {
                Damage(results[i].gameObject);
            }
        }
    }

    void Damage (GameObject target)
    {
        target.GetComponent<Slime>().RecieveDamage(damage);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Vector3 pos = this.transform.position + (Vector3)attackBoxPos;
        Gizmos.DrawWireCube(pos, attackBoxSize);
    }
}
