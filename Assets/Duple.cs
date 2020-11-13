using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Duple<T,Q>
{
    T _obj1;
    Q _obj2;
    public Duple(T obj1,Q obj2){
        _obj1=obj1;
        _obj2=obj2;
    }

    public T obj1{
        get{
            return _obj1;
        }
    }
    public Q obj2{
        get{
            return _obj2;
        }
    }
}
