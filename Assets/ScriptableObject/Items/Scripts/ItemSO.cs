using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName ="ItemSO", menuName = "DodgeController/Items/ItemSO")]
public class ItemSO : ScriptableObject
{
    [Header("Base Info")]
    public Sprite itemSprite;
    public string itemType;
    public Color itemBackColor;
    public Color fontColor;
    public string itemInfoText;
    public string statInfoText;
    public float stat;

    private void OnValidate()
    {
        itemType = this.name;
    }
}
