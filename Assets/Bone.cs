using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bone : MonoBehaviour
{
    // Start is called before the first frame update
    public float length;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3 head{
        get{
            return transform.position;
        }
        set{
            transform.position=value;
        }

    }
    public Vector3 tail{
        get{
            return transform.position+transform.forward*length;
        }
        set{
            transform.position=value-transform.forward*length;
        }

    }
}
