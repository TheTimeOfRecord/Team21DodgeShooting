using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ItemType
{
    StatModifier,
    UsableItem
}

[CreateAssetMenu(fileName ="ItemSO", menuName = "DodgeController/Items/ItemSO")]
public class ItemSO : ScriptableObject
{
    [Header("Base Info")]
    public Sprite itemSprite;
    public ItemType itemType;
    public string itemName;
    public Color itemBackColor;
    public Color fontColor;
    public string itemInfoText;
    public string statInfoText;
    public float stat;

    private void OnValidate()
    {
        itemName = this.name;
    }
}
