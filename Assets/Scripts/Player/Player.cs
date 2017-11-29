using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("GameElements")]
    GameMaster gM;

    [Header("Player")]
    float speed = 5f;

	void Start ()
    {
        gM = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();
    }

	void Update ()
    {
        Movement();
    }

    void Movement()
    {
        Vector3 provisionalPos;

        provisionalPos = this.transform.position;

        provisionalPos.x += gM.GetAxis().x * Time.deltaTime * speed;
        provisionalPos.y += gM.GetAxis().y * Time.deltaTime * speed;

        this.transform.position = provisionalPos;
    }
}
