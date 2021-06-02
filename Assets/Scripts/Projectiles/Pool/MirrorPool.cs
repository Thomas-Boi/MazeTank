using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

/// <summary>
/// A pool used for instantiating Mirror Network
/// objects.
/// </summary>
[CreateAssetMenu(menuName = "Pools/Mirror Pool")]
public class MirrorPool : Pool
{
    // used to hold the pooled objects in the game scene
    // must be added to the Network Manager
    public GameObject emptyIdentityObject;

    [Command]
    public void CmdFillPool()
    {
        pool = new List<GameObject>();
        // make a new object in the scene to store all the pooled objects
        GameObject parent = Instantiate(emptyIdentityObject);
        parent.name = name;
        NetworkServer.Spawn(parent);

        for (int i = 0; i < count.Value; i++)
        {
            GameObject obj = Instantiate(prefab, parent.transform);
            NetworkServer.Spawn(obj);
            obj.SetActive(false);
            pool.Add(obj);
        }
    }
}
