using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public void PrintName()
    {
        print(this.name);
    }

    public void sPrintName(string _input)
    {
        print("str "+_input);
    }

    public void iPrintName(int _input)
    {
        print("int " + _input);
    }
    public void fPrintName(float _input)
    {
        print("float " + _input);
    }
}
