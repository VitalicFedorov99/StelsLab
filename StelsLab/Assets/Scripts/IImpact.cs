using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IImpact
{
    void Impact();

    TypeObjectImpact TypeObj { get;}
}

public enum TypeObjectImpact 
{
    Enemy,
    PC,
    Car
}
