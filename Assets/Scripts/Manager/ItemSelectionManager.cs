using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ItemSelectionManager : MonoBehaviour
{
    public static ItemSelectionManager instance;

    [SerializeField] private GameObject selectPanel;
    [SerializeField] private List<ItemSO> allItems;
    [SerializeField] private List<ItemButtonHandler> optionButtons;
    private int selectionOptionCount = 3;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void DisplaySelectionOptions()
    {
        //selectPanel을 활성화하고 버튼에 아이템을 랜덤으로 넣는다.
        selectPanel.SetActive(true);

        List<ItemSO> selectedItems = GetRandomItems(selectionOptionCount);

        for (int i = 0; i < optionButtons.Count; i++)
        {
            optionButtons[i].SetUpButton(selectedItems[i]);

            int index = i;
            optionButtons[i].GetComponent<Button>().onClick.RemoveAllListeners();
            optionButtons[i].GetComponent<Button>().onClick.AddListener(() => OnItemSelected(selectedItems[index]));
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
                selectedIndexes.Add(randomIndex);
            }
        }
        return selectedItems;
    }

    private void OnItemSelected(ItemSO selectedItemSO)
    {
        //TODO : 아이템 선택 처리 로직
        Debug.Log($"Button {selectedItemSO.itemType} onClick assigned");

        selectPanel.SetActive(false);
    }
}
