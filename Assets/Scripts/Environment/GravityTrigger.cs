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

    List<int> positionZList;

	void Start ()
    {
        positionZList = new List<int>();

        positionZList.Add(10);
        positionZList.Add(9);
        positionZList.Add(8);
        positionZList.Add(7);
        positionZList.Add(6);
        positionZList.Add(5);
        positionZList.Add(4);
        positionZList.Add(3);
        positionZList.Add(2);
        positionZList.Add(1);
        positionZList.Add(0);
        positionZList.Add(-1);
        positionZList.Add(-2);
        positionZList.Add(-3);
        positionZList.Add(-4);
        positionZList.Add(-5);
        positionZList.Add(-6);
        positionZList.Add(-7);
        positionZList.Add(-8);
        positionZList.Add(-9);
        positionZList.Add(-10);

    }

    void LateUpdate ()
    {        
        if(noise)
        {
            noiseCounter += Time.deltaTime;

            if(noiseCounter >= 0.2f)
            {
                noiseCounter = 0;

                this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, positionZList[Random.Range(0, positionZList.Count)]);
            }
        }
        else
        {
            this.transform.position = new Vector3(this.transform.position.x + moveX * Time.deltaTime, this.transform.position.y, this.transform.position.z);
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == ("Wall"))
        {
            other.GetComponent<TilesBehaviour>().StartFalling();
        }
    }
}
