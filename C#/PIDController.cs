using UnityEngine;
using System.Collections;

public class PIDController  {

    private float kP, kI, kD;
    private float previousError, integralAcumulator;

    public PIDController(float p, float i, float d)
    {
        kP = p;
        kI = i;
        kD = d;
    }

    public float Update(float actual, float dest, float deltaTime)
    {
        float p = dest - actual;
        float d = (previousError - p) / deltaTime;
        integralAcumulator += p * deltaTime;
        previousError = p;
        return kP * p + kI * integralAcumulator + kD * d;
    }

	
}
