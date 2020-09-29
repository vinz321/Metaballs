using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetaballsGPU : MonoBehaviour
{
    // Start is called before the first frame update
    public ComputeShader shader;
    public ComputeBuffer pointBuffer;
    ComputeBuffer metaballBuffer;
    public bool changed=true;
    public bool clear=false;
    public float value=0.5f;
    public Vector4[] points=new Vector4[30*30*30];
    public Transform metaball;
    public float metaballRadius;
    public List<Vector4> metaballs=new List<Vector4>();
    List<Vector4> actualMetaballs=new List<Vector4>();
    void Start()
    {
        pointBuffer=new ComputeBuffer(30*30*30,sizeof(float)*4);
        metaballBuffer=new ComputeBuffer(64,sizeof(float)*4);
        actualMetaballs.AddRange(metaballs);
        
    }

    // Update is called once per frame
    void Update()
    {
        if(changed){
            populateMetaballBuffer();
            startShader();
        }
    }
    void OnDestroy()
    {
        ReleaseBuffers();
    }
    public void startShader()
    {
        shader.SetBuffer(0,"points",pointBuffer);
        shader.SetBuffer(0,"metaballs",metaballBuffer);
        shader.SetInt("bufLength",metaballs.Count+1);
        shader.SetVector("pos",transform.position);
        shader.SetInt("numPointsPerAxis",30);
        shader.SetBool("clear",clear);
        int threadGroups=Mathf.CeilToInt(30.0f/8.0f);
        shader.Dispatch(0,threadGroups,threadGroups,threadGroups);

        pointBuffer.GetData(points,0,0,30*30*30);
    }
    void populateMetaballBuffer()
    {
        for(int i=0;i<metaballs.Count;i++)
        {
            actualMetaballs[i]=metaballs[i]-(Vector4)transform.position;
        }
        actualMetaballs.Add(new Vector4(metaball.position.x,metaball.position.y,metaball.position.z,metaballRadius)-(Vector4)transform.position);
        metaballBuffer.SetData<Vector4>(actualMetaballs);
        actualMetaballs.RemoveAt(actualMetaballs.Count-1);
    }
    void ReleaseBuffers()
    {
        if(pointBuffer!=null)
        {
            pointBuffer.Release();
            metaballBuffer.Release();
        }
    }
      void DisposeBuffers()
    {
        if(pointBuffer!=null)
        {
            pointBuffer.Dispose();
            metaballBuffer.Dispose();
        }
    }


    void OnDrawGizmos()
    {
        /*for(int x=0;x<50;x++)
        {
            for(int y=0;y<50;y++)
            {
                for(int z=0;z<50;z++)
                    {
                        if(points[x+y*30+z*30*30].w<value)
                        {
                            Gizmos.DrawSphere(new Vector3(x,y,z),0.1f);
                        }
                    }
            }
        }*/
    }
}
