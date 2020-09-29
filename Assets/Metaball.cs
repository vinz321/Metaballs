using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Metaball
{
    public float radius;
    public float sqrRadius;
    public Vector3 position;

    public Metaball(Vector3 position, float radius)
    {
        this.position=position;
        this.radius=radius;
        sqrRadius=radius*radius;   
    }
    public bool isInside(Vector3 point)
    {
        float t1=position.x-point.x;
        float t2=position.y-point.y;
        float t3=position.z-point.z;
        return (t1*t1+t2*t2+t3*t3)<sqrRadius;
    }
    public float weight(Vector3 point)
    {
        float u=Vector3.Dot(point-position,point-position)/sqrRadius;

        return Mathf.Clamp(1-u,0.0f,1.0f);
    }

}
