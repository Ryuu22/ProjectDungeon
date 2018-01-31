using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundGenerator : MonoBehaviour {

    [Header("Rooms")]
    public GameObject initialRoom;
    public GameObject bossRoom;

    [Header("Corridors")]
    public GameObject[] Corridors;

    public int length;
    private Vector3 newCorridorPosition;

	void Start ()
    {
        //Generate InitialRoom
        initialRoom.gameObject.transform.position = Vector3.zero;
        Instantiate(initialRoom);
        //Generate EveryRoom
        newCorridorPosition = Vector3.zero;

        for(int i = 0; i < length; i++)
        {
            int randomInteger = Random.Range(0,Corridors.Length);
            Corridors[randomInteger].transform.position = newCorridorPosition;
            Instantiate(Corridors[randomInteger]);
            newCorridorPosition.x += 12;
        }


		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
