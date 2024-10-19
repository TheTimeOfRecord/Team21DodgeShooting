using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        FindPlayer();
        objPool = GameObject.FindWithTag("BulletSpawner").GetComponent<ObjectPool>();

        EnemyDeathCount = 0;
    }

    private void Start()
    {
        SceneManager.sceneLoaded += OnBossSceneLoaded;
    }

    private void OnBossSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        FindPlayer();
    }

    private void FindPlayer()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
    }
}
