using UnityEngine;
using System.Collections;

public class Statistics  {
    public static float GaussConstant(float sigma)
    {
        return 1.0f / (Mathf.Sqrt(2 * sigma * sigma * Mathf.PI));
    }
}
