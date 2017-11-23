using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMaster : MonoBehaviour
{
    InputMaster inputM;

    private void Start()
    {
        inputM = GameObject.FindGameObjectWithTag("InputMaster").GetComponent<InputMaster>();
    }

    public void PlayerMovement(GameObject playerGameObject, float speed)
    {
        //float playerX = playerGameObject.transform.position.x;
        //float playerY = playerGameObject.transform.position.y;

        float playerX = inputM.GetAxis().x * Time.deltaTime * speed;
        float playerY = inputM.GetAxis().y * Time.deltaTime * speed;

       // playerGameObject.transform.position = new Vector2(playerX, playerY);

        playerGameObject.transform.Translate(playerX, playerY, 0);
    }
}
