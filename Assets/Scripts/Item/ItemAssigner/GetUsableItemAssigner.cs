using System;
using System.Collections.Generic;
using UnityEngine;

public class GetUsableItemAssigner : MonoBehaviour
{
    private HealthSystem playerHealthSystem;
    private Dictionary<string, Action<float>> getUsableItemActions;
    private void Awake()
    {
        playerHealthSystem = GetComponent<HealthSystem>();

        getUsableItemActions = new Dictionary<string, Action<float>>()
        {
            {"RecoveryPotionItemSO", GetRecoveryPotion },
            {"BombItemSO", GetBomb}
        };
    }

    public void GetItem(ItemSO selectedItemSO)
    {
        if (getUsableItemActions.TryGetValue(selectedItemSO.itemName, out Action<float> getItemAction))
        {
            getItemAction(selectedItemSO.stat);
            UIManager.instance.DiplayUI();
            Debug.Log("아이템 사용!");
        }
        else
        {
            Debug.Log($"{selectedItemSO.itemName}에 대한 GetItem 규칙이 정의되지 않았습니다.");
        }
    }

    public void GetRecoveryPotion(float itemStat)
    {
        playerHealthSystem.ChangeHealth(itemStat);
        Debug.Log("Use RecoveryPotion");
    }
    public void GetBomb(float itemStat)
    {
        // TODO : itemStat으로 폭탄의 데미지를 조정한다.
        // 플레이어의 Bomb의 개수를 관리하는 곳에 Bomb +1
        Debug.Log("Get Bomb");
    }

}