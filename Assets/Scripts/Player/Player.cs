using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("GameElements")]
    GameMaster gM;
    CollisionMaster collision;

    [Header("Player")]
    Vector2 movementSpeed = Vector2.zero;
    Vector2 speed = new Vector2(5, 5);

	void Start ()
    {
        gM = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();
        collision = GetComponent<CollisionMaster>();
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

        if(collision.IsLeftWalled && gM.GetAxis().x < 0) movementSpeed.x = 0;

        if(collision.IsRightWalled && gM.GetAxis().x > 0) movementSpeed.x = 0;

        if(collision.IsTopWalled && gM.GetAxis().y > 0) movementSpeed.y = 0;

        if(collision.IsBottomWalled && gM.GetAxis().y < 0) movementSpeed.y = 0;

        provisionalPos.x += gM.GetAxis().x * Time.deltaTime * movementSpeed.x;
        provisionalPos.y += gM.GetAxis().y * Time.deltaTime * movementSpeed.y;

        this.transform.position = provisionalPos;
    }
}
