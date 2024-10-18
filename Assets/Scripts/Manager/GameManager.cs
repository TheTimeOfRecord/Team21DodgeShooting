using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public ObjectPool objPool { get; private set; }
    public Transform Player { get; private set; }

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

        objPool = GameObject.FindWithTag("BulletSpawner").GetComponent<ObjectPool>();
        Player = GameObject.FindGameObjectWithTag("Player").transform;
    }
}
