using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    InputMaster inputM;

	void Start ()
    {
        inputM = GameObject.FindGameObjectWithTag("InputMaster").GetComponent<InputMaster>();
	}

    public Vector2 GetAxis()
    {
        return inputM.GetAxis();
    }
}
