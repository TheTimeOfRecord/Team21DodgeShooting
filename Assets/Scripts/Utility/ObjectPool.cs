using System.Collections.Generic;
using System.Linq;
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
                GameObject obj = Instantiate(pool.prefab, this.transform);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }
            PoolDictionary.Add(pool.tag, objectPool);
        }
    }

    public GameObject GetObjectFromPool(string tag, Vector2 position)
    {
        //잘못된 태그일경우
        if (!PoolDictionary.ContainsKey(tag))
        {
            return null;
        }

        //모든 총알이 생성되어있어서 사용중인 경우
        if (PoolDictionary[tag].All(x => x.activeSelf == true))
        {
            //새로이 생성한다.
            Pool pool = Pools.Find(x => x.tag == tag);
            GameObject newobj = Instantiate(pool.prefab, position, Quaternion.identity, this.transform);
            newobj.transform.position = position;   //위치 설정 한번 더
            PoolDictionary[tag].Enqueue(newobj);
            newobj.SetActive(true);
            return newobj;
        }

        //제대로된 태그이며, 비활성화된 총알이 존재하는 경우
        if (PoolDictionary[tag].TryDequeue(out GameObject obj))
        {
            //꺼내어준다
            obj.transform.position = position;
            obj.SetActive(true);
            PoolDictionary[tag].Enqueue(obj);
            return obj;
        }
        return null;
    }

    public GameObject GetObjectFromPool(string tag)
    {
        //잘못된 태그일경우
        if (!PoolDictionary.ContainsKey(tag))
        {
            return null;
        }

        //모든 총알이 생성되어있어서 사용중인 경우
        else if (PoolDictionary[tag].All(x => x.activeSelf == true))
        {
            //새로이 생성한다.
            Pool pool = Pools.Find(x => x.tag == tag);
            GameObject newobj = Instantiate(pool.prefab, this.transform);
            PoolDictionary[tag].Enqueue(newobj);
            newobj.SetActive(true);
            return newobj;
        }

        //제대로된 태그이며, 비활성화된 총알이 존재하는 경우
        else if (PoolDictionary[tag].TryDequeue(out GameObject obj))
        {
            //꺼내어준다
            PoolDictionary[tag].Enqueue(obj);
            obj.SetActive(true);
            return obj;
        }
        else return null;
    }
}