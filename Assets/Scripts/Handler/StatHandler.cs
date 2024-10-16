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
        CurrentStat = (HealthStatSO)baseStat.Clone();
    }

}