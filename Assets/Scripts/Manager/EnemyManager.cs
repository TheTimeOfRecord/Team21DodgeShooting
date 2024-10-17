using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;

    [SerializeField] private GameObject[] enemySpawnPoint;

    public ObjectPool EnemyObjectPool { get; private set; }

    [SerializeField] private string enemyTag;

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
            isSpawn = false;
            StartCoroutine(MakeEnemy());
        }
    }

    IEnumerator MakeEnemy()
    {
        if (EnemyObjectPool.PoolDictionary[enemyTag].Any(x => x.activeSelf == false))
        {
            GameObject obj = EnemyObjectPool.GetObjectFromPool(enemyTag);
            obj.transform.position = RandomSpawnInRange();
        }
        yield return new WaitForSeconds(1f);
        isSpawn = true;
    }

    private Vector2 SelectRandomSpawnPoint()
    {
        int index = Random.Range(0, 4);
        return enemySpawnPoint[index].transform.position;
    }

    private Vector2 RandomSpawnInRange()
    {
        float range = Random.Range(-10, 10);
        Vector2 randomPosition = SelectRandomSpawnPoint();
        randomPosition = randomPosition + new Vector2(range, range);
        return randomPosition;
    }
}