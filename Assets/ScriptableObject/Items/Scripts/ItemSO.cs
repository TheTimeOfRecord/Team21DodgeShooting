using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ItemType 
{ 
    //Usable Item
    RecoveryPotion = 0,
    Bomb,

    //StatModifier
    IncreaseBulletNum = 100,
    IncreaseBulletDamage,
    IncreaseSpeed,
    IncreaseHealth
}

[CreateAssetMenu(fileName ="ItemSO", menuName = "DodgeController/Items/ItemSO")]
public class ItemSO : ScriptableObject
{
    [Header("Base Info")]
    public Sprite itemSprite;
    public ItemType itemType;
    public Color itemBackColor;
    public Color fontColor;
    public string itemInfoText;
    public string statInfoText;
    public float stat;
}
