using UnityEngine;
using System.Collections;

/// <summary>
/// This class contains commonly used calculus functions
/// </summary>
public class Calculus  {
	
    /// <summary>
    /// Calculates the rate of change of values a and b 
    /// </summary>
    /// <param name="a">Original value</param>
    /// <param name="b">New value</param>
    /// <param name="t">Time between taking sample a and b</param>
    /// <returns></returns>
    public static float Derivative (float a, float b, float t)
    {
        return (b - a) / t;
    }
    /// <summary>
    /// Calculates the rate of change of values a and b using 
    /// Time.fixedDeltaTime 
    /// </summary>
    /// <param name="a">Original value</param>
    /// <param name="b">New value</param>
    /// <returns></returns>
    public static float Derivative (float a, float b)
    {
        return Derivative(a, b, Time.fixedDeltaTime);
    }
    /// <summary>
    /// Calculates a discrete riemmann sum
    /// </summary>
    /// <param name="x">Value to integrate</param>
    /// <param name="sum">Current accumulated value</param>
    /// <param name="t">Time between samples</param>
    /// <returns></returns>
    public static float Integral( float x, float sum, float t )
    {
        return sum + t * x;
    }
    /// <summary>
    /// Calculates a discrete riemmann sum using Time.fixedDeltaTime
    /// </summary>
    /// <param name="x">Value to integrate</param>
    /// <param name="sum">Current accumulated value</param>
    /// <returns></returns>
    public static float Integral( float x, float sum )
    {
        return Integral( x, sum, Time.fixedDeltaTime);
    }
}
