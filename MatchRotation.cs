using UnityEngine;
using System.Collections;

public class MatchRotation : MonoBehaviour {

    public Transform other;

	
	// Update is called once per frame
	void FixedUpdate () {
        transform.rotation = Quaternion.Slerp(transform.rotation, other.rotation, Time.fixedDeltaTime);
	}
}
