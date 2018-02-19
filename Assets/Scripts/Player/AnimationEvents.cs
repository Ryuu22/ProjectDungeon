using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{

	void Start ()
    {
		
	}
    
    void ResetSpeed()
    {
        GetComponentInParent<Player>().ResetSpeed();
    }

}
