using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorUtils {


    public static Vector3 ClampComponentWise(Vector3 values, Vector3 maxes, Vector3 mins)
    {
        Vector3 rv = values;
        for(int i = 0; i<3; i++)
        {
            rv[i] = Mathf.Clamp(rv[i], mins[i], maxes[i]);
        }
        return rv;
    }

    public static Vector3 LocalToGlobal(Vector3 old, Vector3 reference, Vector3 dirr)
    {

        Quaternion q = Quaternion.FromToRotation(reference, dirr);
        return q * old;
    }

}
