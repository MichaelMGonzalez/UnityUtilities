using UnityEngine;
using System.Collections;

public class VisibleInEditor : MonoBehaviour {
    
    public enum Shape { Sphere, Cube }

    public Shape shape;
    public bool isWireFrame = true;
    public Color color = Color.yellow;
    public Vector3 cubeSize = Vector3.one;
    public float radius = 1;

    void OnDrawGizmos()
    {
        Gizmos.color = color;
        switch(shape)
        {
            case Shape.Cube:
                if (isWireFrame)
                    Gizmos.DrawWireCube(transform.position, cubeSize);
                else
                    Gizmos.DrawCube(transform.position, cubeSize);
                break;
            case Shape.Sphere:
                if(isWireFrame)
                    Gizmos.DrawWireSphere(transform.position, radius);
                else
                    Gizmos.DrawSphere(transform.position, radius);
                break;
        }

    }
	
}
