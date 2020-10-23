using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Limb : MonoBehaviour
{
    public Bone[] bones;
    public Transform effector;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bones[bones.GetUpperBound(0)].transform.LookAt(effector);
        bones[bones.GetUpperBound(0)].tail=effector.position;

        for(int i=bones.GetUpperBound(0)-1;i>=0;i--)
        {
            bones[i].transform.LookAt(bones[i+1].head);
            bones[i].tail=bones[i+1].head;
        }

        bones[0].head=transform.position;

        for(int i=1;i<=bones.GetUpperBound(0);i++){
            
            bones[i].head=bones[i-1].tail;
        }

    }
}
