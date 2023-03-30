using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SAMU : MonoBehaviour
{
    public void CallSamu()
    {
        HasCalledSAMU = true;
    }

    public bool HasCalledSAMU { get; private set; }
}
