using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBehaviour : MonoBehaviour {

    public Transform target;
    float strength = 1f;
    private Quaternion targetRotation;
    private float str;
	
	// Update is called once per frame
	void LateUpdate ()
    {
        //This line finds the rotation from this object to the target
        targetRotation = Quaternion.LookRotation(target.position - transform.position);
        //This line finds out how much you are wanting to lerp the rotation and prevents it from overshooting the rotation
        str = Mathf.Min(strength * Time.deltaTime, 1);
        //This line simply lerps the rotation of the object
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, str);
	}
}
