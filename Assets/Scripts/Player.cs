using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float speed;

    public Transform playerTransform;
    public Vector2 playerVector;

	void Start ()
    {
        playerTransform = this.transform;

    }

	void Update ()
    {
        playerVector.x = playerTransform.transform.position.x;
        playerVector.y = playerTransform.transform.position.y;		
	}

    public void PlayerMovement(Vector2 axis)
    {
        playerVector.x += axis.x * Time.deltaTime * speed;
        playerVector.y += axis.y * Time.deltaTime * speed;

        playerTransform.position = playerVector;
    }
}
