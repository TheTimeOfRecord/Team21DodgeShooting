using System;
using Unity.Mathematics;
using UnityEngine;

public class StatHandler : MonoBehaviour
{
    [SerializeField] private HealthStatSO baseStat;
    public HealthStatSO CurrentStat { get; private set; }

    public ItemToStatAssigner itemToStatAssigner;

    private void Awake()
    {
        UpdateCharacterStat();
    }

    private void UpdateCharacterStat()
    {
        CurrentStat = (HealthStatSO)baseStat.Clone();
    }

    public void ChangeCharacterStat(stats stat, float amount)
    {
        switch (stat)
        {
            case stats.maxHP:
                CurrentStat.maxHP += amount;
                break;
            case stats.ATK:
                CurrentStat.ATK += amount;
                break;
            case stats.bulletSize:
                CurrentStat.bulletSize = amount;
                break;
            case stats.bulletSpeed:
                CurrentStat.bulletSpeed = amount;
                break;
            case stats.bulletNum:
                CurrentStat.bulletNum = (int)amount;
                break;
        }
    }

}

public enum stats
{
    maxHP,
    ATK,
    bulletSize,
    bulletSpeed,
    bulletNum
}