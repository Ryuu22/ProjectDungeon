﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleBehaviour : MonoBehaviour
{
    public void GetDestroyed()
    {
        //Add delay and so on
        Destroy(this.gameObject);
    }
}
