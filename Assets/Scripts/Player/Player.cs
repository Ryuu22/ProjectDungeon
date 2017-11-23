using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("GameElements")]
    GameMaster gm;
    MoveMaster movM;

    [Header("Player")]
    float speed = 5f;
    Vector2 playerVector;

	void Start ()
    {
        gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();
        movM = GameObject.FindGameObjectWithTag("MoveMaster").GetComponent<MoveMaster>();
    }

	void Update ()
    {
        movM.PlayerMovement(this.gameObject, speed);
	}
}
