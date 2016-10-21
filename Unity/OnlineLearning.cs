using UnityEngine;
using System.Collections;

public class OnlineLearning : OnlineLearningAbstractFSM{

    public PIDPositionController c;
    private Rigidbody rb;
    [Range(0, 0.2f)]
    public float alpha = 0.05f;
    public float timeToComplete = 0;
    public float lastTimeToComplete = 5;
    public float deltaError = -1;
    public Vector3 start;
    
	void Start () {
        c = GetComponent<PIDPositionController>();
        c.kP = Random.value;
        c.kI = Random.value;
        c.kD = Random.value;
        rb = GetComponent<Rigidbody>();
        start = transform.position;
	}
	
    protected override IEnumerator ExecuteActionRunningExperiment()
    {
        return null;
    }

    protected override IEnumerator ExecuteActionTrain()
    {
        deltaError = timeToComplete - lastTimeToComplete;
        lastTimeToComplete = timeToComplete;
        c.kP += alpha * deltaError ;
        c.kI += alpha * deltaError ;
        c.kD += alpha * deltaError ;
        return null;
    }

    protected override bool OnTrain()
    {
        timeToComplete = TimeInState();
        return rb.velocity.magnitude < 0.1f || timeToComplete > 10;
    }


    public override void Reset()
    {
    }

    protected override IEnumerator ExecuteActionRestartExperiment()
    {
        c.StopAllCoroutines();
        rb.velocity = Vector3.zero;
        transform.position = start;
        c.StartController();
        return null;
    }
}
