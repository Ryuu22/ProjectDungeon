using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour {

    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform cameraTransform;

    [SerializeField] private float freq;
    [SerializeField] private float offsetX;
    [SerializeField] private float offsetY;

    private bool shaking;
    private float magnitude;
    private float magnitudeRotation;
    private float shakingCounter;
    


    private Vector3 cameraPos;

    void Start ()
    {
        GameObject player = GameObject.FindWithTag("Player");
        playerTransform = player.GetComponent<Transform>();

        cameraTransform = this.GetComponent<Transform>();
	}

    void Update()
    {
        cameraPos = Vector3.Lerp(cameraTransform.position, playerTransform.position, freq);
        cameraPos.z = -10;
        this.transform.position = new Vector3(cameraPos.x + offsetX, cameraPos.y + offsetY, cameraPos.z);

        if (Input.GetKey(KeyCode.AltGr))
        {
            ShakeCamera(0.5f, 30.0f, 1.0f);
        }


        if (shaking)
        {
            ShakeUpdate();
            shakingCounter-=Time.deltaTime;            
        }
        if (shakingCounter < 0)
        {
            shaking = false;
            this.transform.rotation = Quaternion.Euler(Vector3.zero);
        }

    }
    private void ShakeUpdate()
    {
        //Translate Noise
        #region Translate
        float height = Mathf.PingPong(Mathf.PerlinNoise(Random.value, -Random.value) * magnitude, Mathf.PerlinNoise(Random.value, -Random.value) * -magnitude);
        float width = Mathf.PingPong(Mathf.PerlinNoise(Random.value, -Random.value) * magnitude, Mathf.PerlinNoise(Random.value, -Random.value) * -magnitude);

        Vector3 pos = new Vector3(playerTransform.position.x, playerTransform.position.y, -10);
        pos.y = playerTransform.position.y + height + magnitude;
        pos.x = playerTransform.position.x + width + magnitude;

        this.transform.position = pos;
        #endregion

        //Rotation Noise
        #region Rotation
        Vector3 newRotation = Vector3.zero;
        newRotation.z = Mathf.PingPong(Mathf.PerlinNoise(Random.value, Random.value) * magnitudeRotation *-1, Mathf.PerlinNoise(Random.value, Random.value) * magnitudeRotation);


        this.transform.rotation = Quaternion.Euler(newRotation);
        #endregion
    }

    public void ShakeCamera(float newMagnitude, float newMagnitudeRotation, float time)
    {
        shaking = true;
        magnitude = newMagnitude;
        magnitudeRotation = newMagnitudeRotation;

        shakingCounter = time;
    }
}
