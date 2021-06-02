using System.Collections;
using System.Collections.Generic;
using Mirror;

public class PoolManager : NetworkBehaviour
{
    public MirrorPool roundShellPool;

    /// <summary>
    /// Initialize the pools before anything access it.
    /// </summary>
    public override void OnStartServer()
    {
        roundShellPool.CmdFillPool();
    }
}
