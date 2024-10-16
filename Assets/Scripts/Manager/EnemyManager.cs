using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;
    [SerializeField] private Transform enemySpawnPoint;

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
        GameObject obj = EnemyObjectPool.GetObjectFromPool("TracingEnemy");
        obj.transform.position = enemySpawnPoint.position;
        isSpawn = false;
        yield return new WaitForSeconds(3f);
        isSpawn = true;
    }
}
