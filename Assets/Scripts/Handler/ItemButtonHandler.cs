using System;
using UnityEngine;
using UnityEngine.UI;

public class ItemButtonHandler : MonoBehaviour
{
    [SerializeField] private Image buttonImage;
    [SerializeField] private Image itemImage;
    [SerializeField] private Text optionText;
    [SerializeField] private Text statText;

    public void SetUpButton(ItemSO itemSO)
    {
        itemImage.sprite = itemSO.itemSprite;
        buttonImage.color = itemSO.itemBackColor;

        optionText.color = itemSO.fontColor;
        statText.color = itemSO.fontColor;

        optionText.text = itemSO.itemInfoText;

        statText.text = itemSO.statInfoText;
    }


}