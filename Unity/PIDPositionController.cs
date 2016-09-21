using UnityEngine;
using System.Collections;
/// <summary>
/// A PIDController to move an object to a desired location via the physics 
/// engine. The effect looks more organic than standard lerping 
/// Underlying Phyics: https://en.wikipedia.org/wiki/PID_controller
/// \todo { Generalize to a varied purpose PID controller and switch logic to
/// a HLSM }
/// </summary>
public class PIDPositionController : MonoBehaviour {


    public enum TrackingType { Transform, Vector, HoldPosition }

    public float kP = 1.0f;
    public float kI = 1.0f;
    public float kD = 1.0f;

    public float maximumImpulse = 1.0f;
    public float minimumImpulse = 0f;
    public float maxVelocity = 1000f;
    public float velocityDeviation = 0f;

    public float impuseRate = .2f;

    public TrackingType trackingType = TrackingType.Transform;
    public Transform destinationTransform;
    public Vector3 destinationVector;
    public bool lookAtDest;

    // P term variables
    private Vector3 previousError;
    private Vector3 currError;

    // I term variables
    private Vector3 integral = Vector3.zero;

    // D term variables
    private Vector3 derivative = Vector3.zero;
    private float inverseDeltaTime;


    private Rigidbody rb;
    private bool is2DObj;

	void Start() {
        inverseDeltaTime = 1.0f / impuseRate;
        rb = GetComponent<Rigidbody>();
        integral = Vector3.zero;
        if(trackingType == TrackingType.HoldPosition)
        {
            trackingType = TrackingType.Vector;
            destinationVector = transform.position;
        }
        StartCoroutine(Run());
	}

    void OnDisable()
    {
        integral = Vector3.zero;
    }

    IEnumerator Run()
    {
        while (true)
        {
            Vector3 impulseVect = CalculatePID();
            ApplyImpulse(impulseVect);
            //Vector3 dest = transform.position + RandomizeVector(impulseVect, velocityDeviation);
            //transform.position = Vector3.Lerp(transform.position, dest, Time.deltaTime);
            if(lookAtDest)
            {
                switch(trackingType)
                {
                    case TrackingType.Transform:
                        transform.LookAt(destinationTransform);
                        break;
                    case TrackingType.Vector:
                        transform.LookAt(destinationVector);
                        break;
                }
            }
            yield return new WaitForSeconds(impuseRate);
        }
    }

    void OnCollisionEnter(Collision c)
    {
        integral = Vector3.zero;
    }
    void OnTriggerEnter(Collider c)
    {
        integral = Vector3.zero;
    }

    void ApplyImpulse( Vector3 impulse )
    {
        Vector3 clampedVector = Vector3.ClampMagnitude(impulse, maximumImpulse);
        rb.AddForce(clampedVector, ForceMode.Impulse);
    }

    Vector3 CalculatePID()
    {
        CalculateError();
        CalculateDerivative();
        Vector3 p = kP * currError;
        Vector3 i = kI * integral;
        Vector3 d = kD * derivative;
        return p + i + d;

    }

    void CalculateError()
    {
        Vector3 dest = destinationVector;
        switch(trackingType)
        {
            case TrackingType.Transform:
                dest = destinationTransform.position;
                break;
            case TrackingType.Vector:
                dest = destinationVector;
                break;
        }
        currError = dest - transform.position;
    }

    void CalculateDerivative()
    {
        derivative = inverseDeltaTime * (currError - previousError);
        previousError = currError;
    }

    void CalculateIntegral()
    {
        integral += inverseDeltaTime * currError;
    }
    void Destroy()
    {
        Destroy(this.gameObject);
    }
}
