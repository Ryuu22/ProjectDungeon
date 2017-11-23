using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputMaster : MonoBehaviour
{
    GameMaster gm;

    void Start ()
    {
        gm = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();
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
