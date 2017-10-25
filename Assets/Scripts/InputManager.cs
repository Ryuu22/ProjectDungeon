using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    [SerializeField] private Vector2 playerAxis;

    public Player player;
   

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        playerAxis.x = Input.GetAxis("Horizontal");

        playerAxis.y = Input.GetAxis("Vertical");

        player.PlayerMovement(playerAxis);
	}
    

}
