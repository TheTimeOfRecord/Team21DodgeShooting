using System;
using System.Collections.Generic;
using UnityEngine;

public class ItemToStatAssigner : MonoBehaviour
{
    private Dictionary<string, Action<HealthStatSO, float>> statModifiers;

    private void Awake()
    {
        statModifiers = new Dictionary<string, Action<HealthStatSO, float>>
        {
            {"IncreaseBulletNum", (stat, value) => stat.bulletNum += (int)value },
            {"IncreaseBulletDamage", (stat, value) => stat.ATK += value },
            {"IncreaseSpeed", (stat, value) => stat.speed += value },
            {"IncreaseHealth", (stat, value) => stat.maxHP += value }
            //StatModifier추가시 여기에 기능 추가
        };
    }

    public void ModifyStatBasedOnItem(ItemSO itemSO, HealthStatSO currentStat)
    {
        if (itemSO.itemType == ItemType.StatModifier)
        {
            if (statModifiers.TryGetValue(itemSO.itemName, out Action<HealthStatSO, float> modifyAction))
            {
                modifyAction(currentStat, itemSO.stat);
                Debug.Log("스탯변경 완료.");
            }
            else
            {
                Debug.Log($"{itemSO.itemName}에 대한 스탯 변경 규칙이 정의되지 않았습니다.");
            }
        }
    }
}