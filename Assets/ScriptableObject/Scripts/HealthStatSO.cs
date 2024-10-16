using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DefalutStatSO", menuName = "DodgeController/StatSO", order = 0)]
public class HealthStatSO : ScriptableObject
{
    [Header("BaseStat")]
    public float maxHP;
    public float ATK;
    public float speed;
    public float exp;
    public float bulltSize;
    public float delay;
}

public enum needExp
{
    //�������� �ʿ��� ����ġ���� ���صӴϴ�.
}
