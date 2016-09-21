using UnityEngine;
using System.Collections;

public class MatchRotation : MonoBehaviour {

    public Transform other;

    public bool slerp;

	
	// Update is called once per frame
	void FixedUpdate () {
        if(slerp)
        transform.rotation = Quaternion.Slerp(transform.rotation, other.rotation, Time.fixedDeltaTime);
        else
        transform.rotation = other.rotation; 
	}
}
