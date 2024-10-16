using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    public List<Pool> Pools;
    public Dictionary<string, Queue<GameObject>> PoolDictionary;

    private void Awake()
    {
        PoolDictionary = new Dictionary<string, Queue<GameObject>>();
        foreach(var pool in Pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for(int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }
            PoolDictionary.Add(pool.tag, objectPool);
        }
    }

    public GameObject SpawnFromPool(string tag)
    {
        if (!PoolDictionary.ContainsKey(tag))
        {
            return null;
        }
        else if (PoolDictionary[tag].TryDequeue(out GameObject obj))
        {
            PoolDictionary[tag].Enqueue(obj);
            obj.SetActive(true);
            return obj;
        }
        else
        {
            Pool pool = Pools.Find(x => x.tag == tag);
            if(pool != null)
            {
                GameObject newObj = Instantiate(pool.prefab);
                newObj.SetActive(true);
                PoolDictionary[tag].Enqueue(newObj);
                return newObj;
            }
            else
            {
                return null;
            }
        }
    }
}