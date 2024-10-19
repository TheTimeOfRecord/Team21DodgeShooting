using System;
using System.Collections.Generic;
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

    public void ModifiyCharacterStat(ItemSO itemSO)
    {
        itemToStatAssigner.ModifyStatBasedOnItem(itemSO, CurrentStat);
    }
}