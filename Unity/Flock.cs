using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PIDController))]
public class Flock : MonoBehaviour {

    public enum Type { LEADER, FOLLOWER }
    public Type type = Type.FOLLOWER;
    public float personalSpaceRadius = 1;
    public float flockingRadius = 1;
    public float flockingPreference = 4;
    public float avoidancePreference = 2;
    public float velocityScalar = 1;
    public float headingPreference = 1;
    public Vector3 originalDir;


    private Rigidbody rb;
    private PIDController pid;
    public Vector3 prevPos;
    private Flock leader;
	void Start () {
        pid = GetComponent<PIDController>();
        prevPos = transform.position;
        originalDir = transform.forward;
	}
	
	// Update is called once per frame

	void FixedUpdate() {
		//Debug.Log ("hi");
	}
	void Update () {
        //transform.LookAt(rb.velocity);
		prevPos = transform.position;
        //if (type == Type.LEADER)
        //    return;
        //pid.destinationVector = transform.position +
		Vector3 p = transform.forward;;

        if (p == Vector3.zero && leader != null && leader.type == Type.LEADER)
            p = MoveTowardLeader();
        else if (p == Vector3.zero)
        {
            //p = FindFriends();
			const float distribution = 10f;
			if (type == Type.FOLLOWER)
				p += new Vector3( Random.Range(-1 * distribution, distribution), Random.Range(-1 * distribution, distribution), Random.Range(-1 * distribution, distribution) ) ;
        }

        pid.destinationVector = transform.position + p;
        Quaternion q = Quaternion.LookRotation(pid.destinationVector-transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime);
        //transform.LookAt(pid.destinationVector);
	}
    Vector3 FindFriends()
    {
        Vector3 flockPath = Vector3.zero;
        Vector3 groupHeading = Vector3.zero;
        int flockSize = 0;
        Collider[] flock = Physics.OverlapSphere(transform.position, flockingRadius);
        foreach(Collider friend in flock)
        {
            Flock other = friend.GetComponent<Flock>();
            if (other == null)
                continue;
            if(other.type == Type.LEADER)
            {
                leader = other;
                return MoveTowardLeader();
            }
            flockPath += GetVelocity(); 
            flockPath += (other.transform.position - transform.position).normalized;
            groupHeading += other.transform.forward;
            flockSize++;
        }
        groupHeading = headingPreference * (groupHeading * (1.0f / flockSize)).normalized;
        flockPath = (flockPath * (1.0f / flockSize)).normalized;
        return (groupHeading + flockPath + originalDir).normalized;
    }
    Vector3 DetectObstacles()
    {
        Vector3 path = Vector3.zero;
        Collider[] objsInPersonalSpace = Physics.OverlapSphere(transform.position, personalSpaceRadius);
        foreach(Collider obstacle in objsInPersonalSpace )
        {
            Flock other = obstacle.GetComponent<Flock>();
            if (other == null)
                continue;
            Vector3 diff = transform.position - obstacle.transform.position;
            path += diff.normalized;
        }
        path = path.normalized;
        return (avoidancePreference * path).normalized;
    }
    Vector3 MoveTowardLeader()
    {
        return (leader.transform.position - transform.position).normalized;
    }
    public Vector3 GetVelocity()
    {
        return velocityScalar * (transform.position - prevPos).normalized;
    }

}
