using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    private HealthSystem healthSystem;
    [SerializeField] private GameObject RandomItemPrefab;
    private void Awake()
    {
        healthSystem = GetComponent<HealthSystem>();
    }
    private void Start()
    {
        healthSystem.OnDeath += SpawnItem;
    }
    private void SpawnItem(Vector2 position)
    {
        if (IsItemDrop())
        {
            GameManager.Instance.objPool.GetObjectFromPool("RandomItem", position);
        }
    }

    private bool IsItemDrop() //아이템 드랍 확률 정하기
    {
        // 10% 확률로 아이템 드랍
        // TODO : 아이템 드랍 방식을 정해야 함.
        // 아이템 드랍하는 enemy를 만들지, enemy 별로 아이템 드랍 확률을 만들지 등등
        float random = Random.Range(0f, 10f);
        if (random < 1f)
        {
            return true;
        }
        return false;
    }
}