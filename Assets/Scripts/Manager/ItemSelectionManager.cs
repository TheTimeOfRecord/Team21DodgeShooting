using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ItemSelectionManager : MonoBehaviour
{
    [SerializeField] private GameObject selectPanel;
    [SerializeField] private List<ItemSO> allItems;
    [SerializeField] private List<Button> optionButtons;
    private int selectionOptionCount = 3;
    public void DisplaySelectionOptions()
    {
        //selectPanel을 활성화하고 버튼에 아이템을 랜덤으로 넣는다.
        selectPanel.SetActive(true);

        List<ItemSO> selectedItems = GetRandomItems(selectionOptionCount);

        for (int i = 0; i < optionButtons.Count; i++)
        {
            optionButtons[i].GetComponentInChildren<Image>().sprite = selectedItems[i].itemImage.sprite;
        }
    }

    private List<ItemSO> GetRandomItems(int count)
    {
        List<ItemSO> selectedItems = new List<ItemSO>();
        List<int> selectedIndexes = new List<int>();
        while (selectedItems.Count < count)
        {
            int randomIndex = Random.Range(0, allItems.Count);

            if (selectedIndexes.Contains(randomIndex) == false)
            {
                selectedItems.Add(allItems[randomIndex]);
            }
        }
        return selectedItems;
    }
}
