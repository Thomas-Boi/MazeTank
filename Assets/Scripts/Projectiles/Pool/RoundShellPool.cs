using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Pools/RoundShellPool")]
public class RoundShellPool : Pool
{
    public const int roundShellPoolSize = 5;

    protected override void InitCount()
    {
        count = roundShellPoolSize;
    }
}
