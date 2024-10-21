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

    public int PlayerId;

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

    private void Update()
    {
        if(EnemyDeathCount == 50)
        {
            ToBoss();
        }
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

    public void ToBoss()
    {
        StartCoroutine(LoadToBoss());
    }

    public IEnumerator LoadToBoss()
    {
        DestroyAllBullets();

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("BossScene");
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        DestroyAllBullets();

        SceneManager.LoadScene("BossScene");
    }

    private void DestroyAllBullets()
    {
        var allProjectiles = FindObjectsOfType<ProjectileController>();
        foreach (var projectile in allProjectiles)
        {
            projectile.gameObject.SetActive(false);
        }
    }
}
