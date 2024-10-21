using System.Collections.Generic;
using UnityEngine;


public class PlayerLevelAndExpManager : MonoBehaviour
{
    public static PlayerLevelAndExpManager instance;

    [SerializeField] private StatHandler statHandler;

    public List<float> neededExpPerLevel = new List<float>() {
        0, 1, 2, 3, 5,
        8, 13, 21, 34, 55,
        89, 144, 233, 377, 610,
        987, 1597, 2584, 4181, 6765
    };

    private void Awake()
    {
        if (instance == null )
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        UIManager.instance.DiplayUI();
    }

    public void GetExp(float experience)
    {
        statHandler.CurrentStat.exp += experience;
        ApplyExperienceToLevel();
    }

    public void ApplyExperienceToLevel()
    {
        while (statHandler.CurrentStat.exp >= neededExpPerLevel[statHandler.CurrentStat.level])
        {
            statHandler.CurrentStat.exp -= neededExpPerLevel[statHandler.CurrentStat.level];
            statHandler.CurrentStat.level++;
            UIManager.instance.DisplayLevelAndEXP();
            ItemSelectionManager.instance.GetChoices();
        }
        UIManager.instance.DisplayLevelAndEXP();
    }

}