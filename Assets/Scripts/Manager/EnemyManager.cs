using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum EnemyType
{
    StraightEnemy,
    TracingEnemy,
    HoveringEnemy,
    BlinkingEnemy
}

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;
    public ObjectPool EnemyObjectPool { get; private set; }

    [SerializeField] private GameObject[] enemySpawnPoint;
    [SerializeField] private float spawnDelayTime;

    private EnemyType enemyType;
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
        string type = SelectRandomType();
        if (EnemyObjectPool.PoolDictionary[type].Any(x => x.activeSelf == false))
        {
            GameObject obj = EnemyObjectPool.GetObjectFromPool(type);
            obj.transform.position = RandomSpawnInRange();
        }
        yield return new WaitForSeconds(spawnDelayTime);
        isSpawn = true;
    }

    private string SelectRandomType()
    {
        int typeNum = Random.Range(0, 4);
        string type = System.Enum.GetName(typeof(EnemyType), typeNum);
        return type;
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