using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshUtils
{
    static long vectId(Vector3Int v){
        return v.x;
    }
    public static void normalsRecalc(Vector3[] vertices,Vector3[] normals,float d){

        Dictionary<Vector3Int,List<int>> dict=new Dictionary<Vector3Int,List<int>>();

        for(int i=0;i<vertices.Length;i++){
            Vector3Int v=new Vector3Int(Mathf.FloorToInt(vertices[i].x/d),Mathf.FloorToInt(vertices[i].y/d),Mathf.FloorToInt(vertices[i].z/d));
            //int v=vInt.GetHashCode();
            if(dict.ContainsKey(v)){
                dict[v].Add(i);
            }
            else{
                List<int> l=new List<int>();
                l.Add(i);
                dict.Add(v,l);
            }
        }

        foreach(Vector3Int v in dict.Keys){
            List<int> l=dict[v];
            Vector3 normMid=Vector3.zero;
            foreach(int i in l){
                normMid+=normals[i];
            }
            normMid/=l.Count;
            normMid.Normalize();
            foreach(int i in l){
                normals[i]=normMid;
            }
        }
    }
}
