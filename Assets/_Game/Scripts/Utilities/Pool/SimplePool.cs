using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SimplePool
{
    static int DEFAULT_AMOUNT = 10;
    static Dictionary<GameUnit, Pool> poolObject = new Dictionary<GameUnit, Pool>();
    static Dictionary<GameUnit, Pool> poolParent = new Dictionary<GameUnit, Pool>();


    public static void Preload(GameUnit prefab, int amount, Transform parent)
    {
        if (!poolObject.ContainsKey(prefab))
        {
            poolObject.Add(prefab, new Pool(prefab, amount, parent));
        }
    }

    public static GameUnit Spawn(GameUnit prefab, Vector3 position, Quaternion rotation)
    {
        if (!poolObject.ContainsKey(prefab) || poolObject[prefab] == null)
        {
            poolObject.Add(prefab, new Pool(prefab, DEFAULT_AMOUNT, null));
        }

        GameUnit obj = poolObject[prefab].Spawn(position, rotation);

        return obj;
    }

    public static T Spawn<T>(GameUnit prefab, Vector3 position, Quaternion rotation) where T : GameUnit
    {
        if (!poolObject.ContainsKey(prefab) || poolObject[prefab] == null)
        {
            poolObject.Add(prefab, new Pool(prefab, DEFAULT_AMOUNT, null));
        }

        GameUnit obj = poolObject[prefab].Spawn(position, rotation);

        return obj as T;
    }

    public static T Spawn<T>(GameUnit prefab) where T : GameUnit
    {
        if (!poolObject.ContainsKey(prefab) || poolObject[prefab] == null)
        {
            poolObject.Add(prefab, new Pool(prefab, DEFAULT_AMOUNT, null));
        }

        GameUnit obj = poolObject[prefab].Spawn();

        return obj as T;
    }

    public static GameUnit Spawn(GameUnit prefab, Vector3 localPosition, Transform myParent)
    {
        if (!poolObject.ContainsKey(prefab) || poolObject[prefab] == null)
        {
            poolObject.Add(prefab, new Pool(prefab, DEFAULT_AMOUNT, myParent));
        }

        GameUnit obj = poolObject[prefab].Spawn(localPosition, myParent);

        return obj;
    }

    public static T Spawn<T>(GameUnit prefab, Vector3 localPosition, Transform myParent) where T : GameUnit
    {
        if (!poolObject.ContainsKey(prefab) || poolObject[prefab] == null)
        {
            poolObject.Add(prefab, new Pool(prefab, DEFAULT_AMOUNT, myParent));
        }

        GameUnit obj = poolObject[prefab].Spawn(localPosition, myParent);

        return obj as T;
    }

    public static GameUnit Spawn(GameUnit prefab, Transform myParent, int amount)
    {
        if (!poolObject.ContainsKey(prefab) || poolObject[prefab] == null)
        {
            poolObject.Add(prefab, new Pool(prefab, amount, myParent));
        }

        GameUnit obj = poolObject[prefab].Spawn(myParent);

        return obj;
    }

    public static T Spawn<T>(GameUnit prefab, Transform myParent, int amount) where T : GameUnit
    {
        if (!poolObject.ContainsKey(prefab) || poolObject[prefab] == null)
        {
            poolObject.Add(prefab, new Pool(prefab, amount, myParent));
        }

        GameUnit obj = poolObject[prefab].Spawn(myParent);

        return obj as T;
    }

    public static void Despawn(GameUnit obj)
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
        Queue<GameUnit> pool = new Queue<GameUnit>();
        List<GameUnit> activeObjs = new List<GameUnit>();
        Transform parent;
        GameUnit prefab;

        public Pool(GameUnit prefab, int amount, Transform parent)
        {
            this.prefab = prefab;
            this.parent = parent;

            for (int i = 0; i < amount; i++)
            {
                GameUnit obj = GameObject.Instantiate(prefab, parent);
                poolParent.Add(obj, this);
                pool.Enqueue(obj);
                obj.gameObject.SetActive(false);
            }
        }

        public GameUnit Spawn(Vector3 position, Quaternion rotation)
        {
            GameUnit obj = null;

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
            obj.gameObject.SetActive(true);

            activeObjs.Add(obj);

            return obj;
        }

        public GameUnit Spawn(Vector3 localPosition, Transform myParent)
        {
            GameUnit obj = null;

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
            obj.gameObject.SetActive(true);

            activeObjs.Add(obj);

            return obj;
        }

        public GameUnit Spawn(Transform myParent)
        {
            GameUnit obj = null;

            if (pool.Count == 0)
            {
                obj = GameObject.Instantiate(prefab, myParent);
                poolParent.Add(obj, this);
            }
            else
            {
                obj = pool.Dequeue();
            }

            obj.gameObject.SetActive(true);

            activeObjs.Add(obj);

            return obj;
        }

        public GameUnit Spawn()
        {
            GameUnit obj = null;

            if (pool.Count == 0)
            {
                obj = GameObject.Instantiate(prefab, parent);
                poolParent.Add(obj, this);
            }
            else
            {
                obj = pool.Dequeue();
            }

            obj.gameObject.SetActive(true);

            activeObjs.Add(obj);

            return obj;
        }

        public void Despawn(GameUnit obj)
        {
            activeObjs.Remove(obj);
            pool.Enqueue(obj);
            obj.gameObject.SetActive(false);
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
                GameUnit obj = pool.Dequeue();
                GameObject.Destroy(obj);
            }
        }
    }
}

public class GameUnit : MonoBehaviour
{

}
