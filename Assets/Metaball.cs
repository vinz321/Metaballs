using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Metaball : MonoBehaviour
{
    public float radius;
    public float sqrRadius;

    public void Start()
    {
        
    }
    public Vector4 metaball{
        get{
            return new Vector4(transform.position.x,transform.position.y,transform.position.z,radius);
        }
    }
}
