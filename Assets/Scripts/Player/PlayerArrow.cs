﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArrow : MonoBehaviour
{
    Vector2 arrowPos;
    Vector2 direction;
    float speed = 10;

	void Start ()
    {
        arrowPos = Camera.main.WorldToScreenPoint(transform.position);
        direction = (new Vector2(Input.mousePosition.x, Input.mousePosition.y) - arrowPos).normalized;
    }

	void Update ()
    {
        this.transform.Translate(new Vector3(direction.x, direction.y, 0) * speed * Time.deltaTime);
	}
}