using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pool : ScriptableObject
{
    public GameObject prefab;
    public List<GameObject> pool;
    public int count;

    // parent of the objects in the scene.
    private Transform parent; 

    public void FillPool()
    {
        InitCount();
        pool = new List<GameObject>();
        parent = new GameObject(name).transform;

        for (int i = 0; i < count; i++)
        {
            GameObject obj = Instantiate(prefab, parent);
            obj.SetActive(false);
            pool.Add(obj);
        }
    }

    /// <summary>
    /// Init the pool by setting the pool count.
    /// Subclasses should set it to their preferred pool count.
    /// </summary>
    protected abstract void InitCount();

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
