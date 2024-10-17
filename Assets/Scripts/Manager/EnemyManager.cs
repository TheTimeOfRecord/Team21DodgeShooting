using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;
    [SerializeField] private GameObject[] enemySpawnPoint;
    private Transform spawnPoint;

    public ObjectPool EnemyObjectPool { get; private set; }

    private bool isSpawn = true;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        Instance = this;

        EnemyObjectPool = GetComponent<ObjectPool>();
    }

    private void Update()
    {
        if (isSpawn)
        {
            StartCoroutine(MakeEnemy());
        }
    }

    IEnumerator MakeEnemy()
    {
        spawnPoint = RandomSpawnRange();
        GameObject obj = EnemyObjectPool.GetObjectFromPool("TracingEnemy");
        obj.transform.position = spawnPoint.position;
        isSpawn = false;
        yield return new WaitForSeconds(3f);
        isSpawn = true;
    }

    private Transform SelectRandomSpawnPoint()
    {
        int index = Random.Range(0, 4);
        return enemySpawnPoint[index].transform;
    }

    private Transform RandomSpawnRange()
    {
        float range = Random.Range(-10, 10);
        Transform randomSpawnTransform = SelectRandomSpawnPoint();
        randomSpawnTransform.position = randomSpawnTransform.position + new Vector3(range, range, 10);
        return randomSpawnTransform;
    }
}