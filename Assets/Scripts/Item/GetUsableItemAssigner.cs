using System;
using System.Collections.Generic;
using UnityEngine;

public class GetUsableItemAssigner : MonoBehaviour
{
    [SerializeField] private HealthSystem playerHealthSystem;
    private Dictionary<string, Action<float, HealthStatSO>> getUsableItemActions;
    private void Awake()
    {
        getUsableItemActions = new Dictionary<string, Action<float, HealthStatSO>>()
        {
            {"RecoveryPotion", GetRecoveryPotion },
            {"Bomb", GetBomb}
        };
    }

    public void GetRecoveryPotion(float itemStat, HealthStatSO currentStat)
    {
        playerHealthSystem.ChangeHealth(itemStat);
        Debug.Log("Use RecoveryPotion");
    }
    public void GetBomb(float itemStat, HealthStatSO currentStat)
    {
        // TODO : itemStat으로 폭탄의 데미지를 조정한다.
        // 플레이어의 Bomb의 개수를 관리하는 곳에 Bomb +1
        Debug.Log("Get Bomb");
    }

}