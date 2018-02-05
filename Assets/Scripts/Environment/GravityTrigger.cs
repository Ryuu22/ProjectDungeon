using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityTrigger : MonoBehaviour
{

    [SerializeField]
    float moveX;
    [SerializeField]
    bool noise;
    float noiseCounter;

    List<float> positionYList;

	void Start ()
    {
        positionYList = new List<float>();

        positionYList.Add(2.5f);
        positionYList.Add(1.5f);
        positionYList.Add(0.5f);
        positionYList.Add(-1.5f);


    }

    void LateUpdate ()
    {        
        if(noise)
        {
            noiseCounter += Time.deltaTime;

            if(noiseCounter >= 0.2f)
            {
                noiseCounter = 0;

                this.transform.position = new Vector3(this.transform.position.x, positionYList[Random.Range(0, positionYList.Count)], this.transform.position.z);
            }
        }
        else
        {
            this.transform.position = new Vector3(this.transform.position.x + moveX * Time.deltaTime, this.transform.position.y, this.transform.position.z);
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == ("Destructible"))
        {
            collision.GetComponent<TilesBehaviour>().StartFalling();
        }
    }
}
