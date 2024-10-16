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
    public Image itemImage;
    public ItemType itemType;
    public Color itemColor;
    public Text itemInfo;
    public float stat;
    
}
