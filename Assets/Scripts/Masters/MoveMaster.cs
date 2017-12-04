using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMaster : MonoBehaviour
{
    public void Move(GameObject movingObject, Vector2 target, float speed)
    {
        Vector2 movingObjectPos = movingObject.transform.position;

        movingObject.transform.position = Vector2.MoveTowards(movingObjectPos, target, speed * Time.deltaTime);
    }
}
