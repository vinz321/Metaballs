  using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Metaballs : MonoBehaviour
{
    // Start is called before the first frame update
    Mesh m;
    List<float> vertList=new List<float>();
    List<int> trisList=new List<int>();
    float[] vertices;
    int[]  triangles;
    int size=50;
    public float [,,] points=new float[50,50,50];
    public List<Metaball> metaballs=new List<Metaball>();
    
    void Start()
    {
        m=new Mesh();
    }

    // Update is called once per frame
    void Update()
    {   
        if(Input.GetKeyDown(KeyCode.Alpha0)){
            for (int x=0;x<size;x++)
            {
                for (int y=0;y<size;y++)
                {
                    for (int z=0;z<size;z++)
                    {
                        foreach(Metaball m in metaballs)
                        {
                            points[x,y,z]=points[x,y,z]+m.weight(new Vector3(x,y,z));
                        }
                    }
                }
            }
        }
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            //metaballs.Add(new Metaball(new Vector3(10,10,10),5.0f));
            metaballs.Add(new Metaball(new Vector3(Random.Range(0.0f,size),Random.Range(0.0f,size),Random.Range(0.0f,size)),Random.Range(0.5f,50.0f)));
        }
        
    }

    void OnDrawGizmos()
    {
        /*for (int x=0;x<30;x++)
        {
            for (int y=0;y<30;y++)
            {
                for (int z=0;z<30;z++)
                {
                    if(points[x,y,z]>0.5f)
                        Gizmos.DrawSphere(new Vector3(x,y,z),0.1f);
                }
            }
        }*/
    }
}
