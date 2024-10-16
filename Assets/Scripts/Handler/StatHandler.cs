using System;
using UnityEngine;

public class StatHandler : MonoBehaviour
{
    [SerializeField] private HealthStatSO baseStat;
    public HealthStatSO CurrentStat { get; private set; }

    private void Awake()
    {
        UpdateCharacterStat();
    }

    private void UpdateCharacterStat()
    {
        CurrentStat = new HealthStatSO();
        CurrentStat.maxHP = baseStat.maxHP;
        CurrentStat.ATK = baseStat.ATK;
        CurrentStat.speed = baseStat.speed;
        CurrentStat.exp = baseStat.exp;
        CurrentStat.bulltSize = baseStat.bulltSize;
        CurrentStat.delay = baseStat.delay;
    }
}