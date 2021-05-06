using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject Prefab;
        public int size;
    }

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    public static ObjectPooler Instance;

 
    public void SingletonInit()
    {
        if (!Instance)
        {
            Instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    void Init()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach(var pool in pools)
        {
            Queue<GameObject> objectpool = new Queue<GameObject>();
            for(int i= 0; i  < pool.size; ++i)
            {
                GameObject obj = Instantiate(pool.Prefab);
                obj.SetActive(false);
                objectpool.Enqueue(obj);

            }
            poolDictionary.Add(pool.tag, objectpool);
        }
    }

    public GameObject SpawnFromPool(string tag, Vector3 Position , Quaternion rotation)
    {
        GameObject obj =   poolDictionary[tag].Dequeue();
        if (obj != null)
        {
            obj.SetActive(true);
            obj.transform.position = Position;
            obj.transform.rotation = rotation;

            poolDictionary[tag].Enqueue(obj);
            return obj;
        }
        return null;
    }
}
