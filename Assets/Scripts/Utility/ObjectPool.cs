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
        //�߸��� �±��ϰ��
        if (!PoolDictionary.ContainsKey(tag))
        {
            return null;
        }

        //��� �Ѿ��� �����Ǿ��־ ������� ���
        if (PoolDictionary[tag].All(x => x.activeSelf == true))
        {
            //������ �����Ѵ�.
            Pool pool = Pools.Find(x => x.tag == tag);
            GameObject newobj = Instantiate(pool.prefab, position, Quaternion.identity, this.transform);
            newobj.transform.position = position;   //��ġ ���� �ѹ� ��
            PoolDictionary[tag].Enqueue(newobj);
            newobj.SetActive(true);
            return newobj;
        }

        //����ε� �±��̸�, ��Ȱ��ȭ�� �Ѿ��� �����ϴ� ���
        if (PoolDictionary[tag].TryDequeue(out GameObject obj))
        {
            //�������ش�
            obj.transform.position = position;
            obj.SetActive(true);
            PoolDictionary[tag].Enqueue(obj);
            return obj;
        }
        return null;
    }

    public GameObject GetObjectFromPool(string tag)
    {
        //�߸��� �±��ϰ��
        if (!PoolDictionary.ContainsKey(tag))
        {
            return null;
        }

        //��� �Ѿ��� �����Ǿ��־ ������� ���
        else if (PoolDictionary[tag].All(x => x.activeSelf == true))
        {
            //������ �����Ѵ�.
            Pool pool = Pools.Find(x => x.tag == tag);
            GameObject newobj = Instantiate(pool.prefab, this.transform);
            PoolDictionary[tag].Enqueue(newobj);
            newobj.SetActive(true);
            return newobj;
        }

        //����ε� �±��̸�, ��Ȱ��ȭ�� �Ѿ��� �����ϴ� ���
        else if (PoolDictionary[tag].TryDequeue(out GameObject obj))
        {
            //�������ش�
            PoolDictionary[tag].Enqueue(obj);
            obj.SetActive(true);
            return obj;
        }
        else return null;
    }
}