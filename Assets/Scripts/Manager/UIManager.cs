using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public HealthSystem healthSystem;
    public StatHandler statHandler;


    public Text levelTxt;
    public RectTransform frontExpBarRT;
    public Text expBarTxt;

    public RectTransform frontHpBarRT;
    public Text hpBarTxt;

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

    public void DiplayUI()
    {
        DisplayLevelAndEXP();
        DisplayHP();
    }

    public void DisplayLevelAndEXP()
    {
        HealthStatSO healthStatSO = statHandler.CurrentStat;
        levelTxt.text = $"Lv.{healthStatSO.level}";
        float neededExp = PlayerLevelAndExpManager.instance.neededExpPerLevel[healthStatSO.level];
        expBarTxt.text = $"{healthStatSO.exp:F2}/{neededExp:F2}";
        frontExpBarRT.localScale = new Vector3(healthStatSO.exp / neededExp, 1.0f, 1.0f);
    }

    public void DisplayHP()
    {
        Debug.Log($"DisplayHP {healthSystem.CurrentHealth}");
        hpBarTxt.text = $"{healthSystem.CurrentHealth:F2}/{healthSystem.MaxHealth:F2}";
        frontHpBarRT.localScale = new Vector3 (healthSystem.CurrentHealth/healthSystem.MaxHealth, 1.0f, 1.0f);
    }
}