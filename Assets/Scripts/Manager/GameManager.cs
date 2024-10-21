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
    private bool isBossAppeared = false;

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


        EnemyDeathCount = 0;
    }

    private void Update()
    {
        if (isBossAppeared)
        {
            return;
        }
        else if(EnemyDeathCount >= 10)
        {
            isBossAppeared = !isBossAppeared;
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
        objPool = GameObject.FindWithTag("BulletSpawner").GetComponent<ObjectPool>();
    }

    private void FindPlayer()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            Player = playerObj.transform;
        }
        else
        {
            Debug.LogWarning("Player object not found. Make sure it is instantiated before calling FindPlayer.");
        }
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
