using UnityEngine;
using System.Collections;

public class Statistics  {
    public static float GaussConstant(float sigma)
    {
        return 1.0f / (Mathf.Sqrt(2 * sigma * sigma * Mathf.PI));
    }
    public static float Gauss(float mean, float sigma, float val)
    {
        return Mathf.Exp(-Mathf.Pow(mean - val, 2) / (2 * sigma * sigma));
    }
    public static float Sigmoid(float a)
    {
        return 1f / (1f + Mathf.Exp(-a));
    }
    public static float SafeExp(float value, float maximumValue, float tau)
    {
        return Mathf.Exp( (value - maximumValue + 1)/tau );
    }
}
