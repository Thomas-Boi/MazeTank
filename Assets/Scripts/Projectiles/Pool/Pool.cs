using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

[CreateAssetMenu(menuName = "Pools/Normal Pool")]
public class Pool : ScriptableObject
{
    // the prefab that will be instantiated
    public GameObject prefab;

    // keeps track of the objects
    protected List<GameObject> pool;

    // holds the size of the pool
    public FloatDataSO count;

    public void FillPool()
    {
        pool = new List<GameObject>();
        // make a new object in the scene to store all the pooled objects
        GameObject parent = new GameObject(name);

        for (int i = 0; i < count.Value; i++)
        {
            GameObject obj = Instantiate(prefab, parent.transform);
            obj.SetActive(false);
            pool.Add(obj);
        }
    }

    /// <summary>
    /// Get a pool object and give it a position and rotation.
    /// </summary>
    /// <param name="newPos"></param>
    /// <param name="newRot"></param>
    /// <returns>An active, unused pool object. Must be set active manually.
    /// If there's no available pool object, returns null.</returns>
    public GameObject GetPoolObj(Vector3 newPos, Quaternion newRot)
    {
        foreach (GameObject obj in pool)
        {
            if (!obj.activeInHierarchy)
            {
                obj.transform.position = newPos;
                obj.transform.rotation = newRot;
                obj.SetActive(true);
                return obj;
            }
        }
        return null;
    }
}
