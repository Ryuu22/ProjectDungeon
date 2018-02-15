using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMaster : MonoBehaviour
{
    public void Move(GameObject movingObject, Vector3 target, float speed)
    {
        Vector3 movingObjectPos = movingObject.transform.position;

        movingObject.transform.position = Vector3.MoveTowards(movingObjectPos, target, speed * Time.deltaTime);
    }
}
