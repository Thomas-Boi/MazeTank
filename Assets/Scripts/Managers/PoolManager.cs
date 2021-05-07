using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public RoundShellPool roundShellPool;

    /// <summary>
    /// Initialize the pools before anything access it.
    /// </summary>
    private void Awake()
    {
        roundShellPool.FillPool();
    }
}
