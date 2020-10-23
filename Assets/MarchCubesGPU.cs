using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MarchCubesGPU : MonoBehaviour
{
    public ComputeShader shader;
    public MetaballsGPU meta;
    MeshFilter meshFilter;
    ComputeBuffer tris;
    ComputeBuffer norm;
    ComputeBuffer trisCount;
    int numPointsPerAxis=30;
    public bool start=false;
    int[] triCountArray={0};
    Triangle[] triArray=new Triangle[65536];
    Vector3[] vertices=new Vector3[65536];
    int[] indices=new int[65536];
    struct Triangle{
        public Vector3 v1;
        public Vector3 v2;
        public Vector3 v3;

        public Vector3 this [int i]
        {
            get{switch(i)
                {
                    case 0:
                        return v1;
                    case 1:
                        return v2;
                    default: 
                        return v3;
                }
            }
        }
    };
    
    void Start()
    {
        meshFilter=GetComponent<MeshFilter>();
        ReleaseBuffers();
        CreateBuffers();
    }
    void OnDestroy()
    {
        ReleaseBuffers();
    }
    // Update is called once per frame
    void Update()
    {
        if(meta.pointBuffer!=null && start){
            computeShaderWork();}
    }
    void CreateBuffers(){
        tris=new ComputeBuffer(5*29*29*29,sizeof(float)*9,ComputeBufferType.Append);
        trisCount=new ComputeBuffer(1,sizeof(int),ComputeBufferType.Raw);
        
    }
    void ReleaseBuffers()
    {
        if(tris!=null)
        {
            tris.Release();
            trisCount.Release();
        }
    }
    void computeShaderWork()
    {
        int numVoxelsPerAxis=numPointsPerAxis-1;
        int threadGroups=Mathf.CeilToInt(29.0f/8.0f);
        
        tris.SetCounterValue(0);
        shader.SetBuffer(0,"tris",tris);
        shader.SetBuffer(0,"points",meta.pointBuffer);
        shader.SetInt("numPointsPerAxis",numPointsPerAxis);
        shader.SetFloat("isolevel",0.5f);
        
        shader.Dispatch(0,threadGroups,threadGroups,threadGroups);

        
        ComputeBuffer.CopyCount(tris,trisCount,0);
        trisCount.GetData(triCountArray);
        int triNum=triCountArray[0];
        triArray=new Triangle[triNum];
        tris.GetData(triArray,0,0,triNum);


        vertices=new Vector3[triNum*3];
        indices=new int[triNum*3];

        for(int i=0;i<triNum;i++)
        {
            for(int j=0;j<3;j++)
            {
                vertices[i*3+j]=triArray[i][j];
                indices[i*3+j]=i*3+j;
            }
        }

        Mesh mesh=new Mesh();
        mesh.vertices=vertices;
        mesh.triangles=indices;
        mesh.RecalculateNormals();
        meshFilter.mesh=mesh;
        
    }
}
