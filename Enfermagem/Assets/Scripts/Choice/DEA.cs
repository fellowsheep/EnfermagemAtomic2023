using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEA : MonoBehaviour
{
    public void CheckDEA()
    {
        HasDea = true;
    }

    public bool HasDea { get; private set; }
}
