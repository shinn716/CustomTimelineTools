using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public void PrintName()
    {
        print(this.name);
    }

    public void PrintName(string _input)
    {
        print(_input);
    }

    public void PrintName(int _input)
    {
        print(_input);
    }
    public void PrintName(float _input)
    {
        print(_input);
    }
}
