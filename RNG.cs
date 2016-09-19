using UnityEngine;
using System.Collections;
/// <summary>
/// This class provides utility functions to generate random numbers from 
/// non-uniform distributions. 
/// </summary>
public class RNG {
    private const int size = 2000;
    private static float[] gaussianTable;
    /// <summary>
    /// This function generates a zero mean gaussian number using the box
    /// muller transform
    /// </summary>
    /// <param name="stdDev">The standard deviation</param>
    /// <returns>A gaussian number</returns>
    public static float GenerateGaussian( float stdDev )
    {
        EnsureTableExists();
        return stdDev * gaussianTable[(int)Random.Range(0, size)];
    }
    /// <summary>
    /// This function generates gaussian numbers using the box muller transform
    /// </summary>
    /// <param name="mean">The expected value from the distribution</param>
    /// <param name="stdDev">The standard deviation</param>
    /// <returns>A random gaussian number</returns>
    public static float GenerateGaussian( float mean, float stdDev )
    {
        return GenerateGaussian(stdDev) + mean;
    }
    public static Vector3 UniformRandomVector(float min, float max)
    {
        Vector3 rv = new Vector3(Random.value, Random.value, Random.value);
        return Random.Range(min, max) * rv.normalized;
    }
    public static Vector3 RandomVector(Vector3 v, float dev)
    {
        return RNG.GenerateGaussian(dev) * v;
        //return Random.Range(dev/-2, dev/2) * v;
    }
    public static Vector3 TrueRandomVector(float mean, float dev)
    {
        return new Vector3(
            RNG.GenerateGaussian(mean, dev),
            RNG.GenerateGaussian(mean, dev),
            RNG.GenerateGaussian(mean, dev)); 
    }
    public static Vector3 TrueRandomVector(float dev)
    {
        return TrueRandomVector(0, dev);
    }
    private static void EnsureTableExists()
    {
        if (gaussianTable == null)
        {
            gaussianTable = new float[size];
            for( int i = 0; i < size; i++ )
            {
                float twoPi = 2 * Mathf.PI;
                float r1 = Random.value; // Random number from uniform distrubtion (0,1)
                float r2 = Random.value;
                float g = Mathf.Sqrt(-2 * Mathf.Log(r1)) * Mathf.Cos(twoPi * r2);
                gaussianTable[i] = g;
            }
        }
    }
}
