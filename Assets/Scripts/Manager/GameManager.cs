using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public ObjectPool objPool { get; private set; }
    public Transform Player { get; private set; }

    public int EnemyDeathCount;

    public static GameManager Instance
    {
        get
        {
            if(instance == null)
            {
                GameObject go = new GameObject("GameManager");
                instance = go.AddComponent<GameManager>();
                DontDestroyOnLoad(go);
            }
            return instance;
        }
    }

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        Player = GameObject.FindGameObjectWithTag("Player").transform;
        objPool = GameObject.FindWithTag("BulletSpawner").GetComponent<ObjectPool>();

        EnemyDeathCount = 0;
    }
}
