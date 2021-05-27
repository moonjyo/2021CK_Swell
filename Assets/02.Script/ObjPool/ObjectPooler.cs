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

    [System.Serializable]
    public class ObservePool
    {
        public string Objname;
        public GameObject Prefab;
    }

    public List<Pool> pools; // inspector 에서 받아옴
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    public List<ObservePool> ObservePools;
    public Dictionary<string, GameObject> ObservePoolDictionary;

    public static ObjectPooler Instance;

    private void Awake()
    {
        SingletonInit();
    }

    public void SingletonInit()
    {
        if (!Instance)
        {
            Instance = this;
        }
    }

    void Init()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
        ObservePoolDictionary = new Dictionary<string, GameObject>();

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

        foreach(var Pool in ObservePools)
        {
            GameObject Obj = Instantiate(Pool.Prefab);
            Obj.SetActive(false);

            ObservePoolDictionary.Add(Pool.Objname, Obj);
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

    public GameObject SpawnObserveObj(string name)
    {
        ObservePoolDictionary.TryGetValue(name, out GameObject obj);
        if(obj != null)
        {
            obj.SetActive(true);
            return obj;
        }

        return null;
    }
}
