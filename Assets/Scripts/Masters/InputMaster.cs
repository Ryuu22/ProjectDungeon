using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputMaster : MonoBehaviour
{

    Player player;

    void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void Update ()
    {
        if(!player.IsDead)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Fire1"))
            {
                player.Attack();
            }
            if(Input.GetKeyDown(KeyCode.G))
            {
                player.Fall();
            }
            if(Input.GetKeyDown(KeyCode.LeftShift) || Input.GetButtonDown("Fire2"))
            {
                player.BeginDash();
            }
        }
    }

    public Vector2 GetAxis()
    {
        float AxisX = 0;
        float AxisY = 0;

        AxisX = Input.GetAxis("Horizontal");
        AxisY = Input.GetAxis("Vertical");

        return new Vector2(AxisX, AxisY);
    }
}
