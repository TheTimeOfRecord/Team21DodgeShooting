using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DefalutStatSO", menuName = "DodgeController/StatSO", order = 0)]
public class HealthStatSO : ScriptableObject, ICloneable
{
    [Header("BaseStat")]
    public float maxHP;
    public float ATK;
    public float speed;
    public float exp;
    public int level;
    public float bulletSize;
    public float bulletSpeed;
    public int bulletNum;
    public float attackDelay;
    public int bombNum;

    public object Clone()
    {
        return this.MemberwiseClone();
    }
}


public enum needExp
{
    //레벨업에 필요한 경험치량을 정해둡니다.
}


