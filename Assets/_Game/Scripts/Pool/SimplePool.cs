using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SimplePool
{
    static int DEFAULT_AMOUNT = 10;
    static Dictionary<GameObject, Pool> poolObject = new Dictionary<GameObject, Pool>();
    static Dictionary<GameObject, Pool> poolParent = new Dictionary<GameObject, Pool>();


    public static void Preload(GameObject prefab, int amount, Transform parent)
    {
        if (!poolObject.ContainsKey(prefab))
        {
            poolObject.Add(prefab, new Pool(prefab, amount, parent));
        }
    }

    public static GameObject Spawn(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        if (!poolObject.ContainsKey(prefab) || poolObject[prefab] == null)
        {
            poolObject.Add(prefab, new Pool(prefab, DEFAULT_AMOUNT, null));
        }

        GameObject obj = poolObject[prefab].Spawn(position, rotation);

        return obj;
    }

    public static GameObject Spawn(GameObject prefab, Vector3 localPosition, Transform myParent)
    {
        if (!poolObject.ContainsKey(prefab) || poolObject[prefab] == null)
        {
            poolObject.Add(prefab, new Pool(prefab, DEFAULT_AMOUNT, myParent));
        }

        GameObject obj = poolObject[prefab].Spawn(localPosition, myParent);

        return obj;
    }

    public static void Despawn(GameObject obj)
    {
        if (poolParent.ContainsKey(obj))
        {
            poolParent[obj].Despawn(obj);
        }
        else
        {
            GameObject.Destroy(obj);
        }
    }

    public static void CollectAll()
    {
        foreach (var item in poolObject)
        {
            item.Value.Collect();
        }
    }

    public static void ReleaseAll()
    {
        foreach (var item in poolObject)
        {
            item.Value.Release();
        }
    }

    public class Pool
    {
        Queue<GameObject> pool = new Queue<GameObject>();
        List<GameObject> activeObjs = new List<GameObject>();
        Transform parent;
        GameObject prefab;

        public Pool(GameObject prefab, int amount, Transform parent)
        {
            this.prefab = prefab;

            for (int i = 0; i < amount; i++)
            {
                GameObject obj = GameObject.Instantiate(prefab, parent);
                poolParent.Add(obj, this);
                pool.Enqueue(obj);
                obj.SetActive(false);
            }
        }

        public GameObject Spawn(Vector3 position, Quaternion rotation)
        {
            GameObject obj = null;

            if (pool.Count == 0)
            {
                obj = GameObject.Instantiate(prefab, parent);
                poolParent.Add(obj, this);
            }
            else
            {
                obj = pool.Dequeue();
            }

            obj.transform.SetPositionAndRotation(position, rotation);
            obj.SetActive(true);

            activeObjs.Add(obj);

            return obj;
        }

        public GameObject Spawn(Vector3 localPosition, Transform myParent)
        {
            GameObject obj = null;

            if (pool.Count == 0)
            {
                obj = GameObject.Instantiate(prefab, myParent);
                poolParent.Add(obj, this);
            }
            else
            {
                obj = pool.Dequeue();
            }

            obj.transform.localPosition = localPosition;
            obj.SetActive(true);

            activeObjs.Add(obj);

            return obj;
        }

        public void Despawn(GameObject obj)
        {
            activeObjs.Remove(obj);
            pool.Enqueue(obj);
            obj.SetActive(false);
        }

        public void Collect()
        {
            while (activeObjs.Count > 0)
            {
                Despawn(activeObjs[0]);
            }
        }

        public void Release()
        {
            Collect();

            while (pool.Count > 0)
            {
                GameObject obj = pool.Dequeue();
                GameObject.Destroy(obj);
            }
        }
    }
}
