using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour {

    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform cameraTransform;

    [SerializeField] private float freq;

    private bool shaking;
    private float magnitude;

    private Vector2 cameraPos;

    void Start ()
    {
        GameObject player = GameObject.FindWithTag("Player");
        playerTransform = player.GetComponent<Transform>();

        cameraTransform = this.GetComponent<Transform>();
	}

	void Update ()
    {
        

        if(Input.GetKey(KeyCode.AltGr))
        {
            ShakeCamera(5.0f);
        }
        else
        {
            shaking = false;
            cameraPos = Vector2.Lerp(cameraTransform.position, playerTransform.position, freq);

            cameraTransform.position = new Vector3(cameraPos.x, cameraPos.y, -10);
        }
        
        if(shaking)
        {
            float height = Mathf.PingPong(Random.value, Random.value); //magnitude * Mathf.PerlinNoise(Time.time * magnitude , -magnitude);
            float width = Mathf.PingPong(Random.value, Random.value);//magnitude * Mathf.PerlinNoise(Time.time * magnitude, -magnitude);

            Vector3 pos = new Vector3(playerTransform.position.x, playerTransform.position.y,-10);
            pos.y = playerTransform.position.y + height;
            pos.x = playerTransform.position.x + width;
            
            this.transform.position = pos;
        }
	}
    public void ShakeCamera(float newMagnitude)
    {
        shaking = true;
        magnitude = newMagnitude;
    }
}
