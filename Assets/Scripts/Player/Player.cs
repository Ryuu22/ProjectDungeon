using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("GameElements")]
    GameMaster gM;

    [Header("Player")]
    float speed = 5f;
    Rigidbody2D rb;

	void Start ()
    {
        gM = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();
        rb = GetComponent<Rigidbody2D>();
    }

	void Update ()
    {

    }

    void FixedUpdate()
    {
        Movement();
    }

    void Movement()
    {
        rb.AddForce(gM.GetAxis() * speed, ForceMode2D.Impulse);
    }
}
