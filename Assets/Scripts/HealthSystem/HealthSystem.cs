using System;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private float healthChangeDelay = 0.5f;

    public StatHandler statHandler;
    private float timeSinceLastChange = float.MaxValue;
    private bool isAttacked = false;

    public event Action OnDamage;
    public event Action OnHeal;
    public event Action<Vector2> OnDeath;
    public event Action OnInvincibilityEnd;

    public float CurrentHealth { get; private set; }
    public float MaxHealth => statHandler.CurrentStat.maxHP;

    private void Awake()
    {
        statHandler = GetComponent<StatHandler>();
    }

    private void Start()
    {
        CurrentHealth = MaxHealth;
    }

    private void Update()
    {
        if(isAttacked && (timeSinceLastChange < healthChangeDelay))
        {
            timeSinceLastChange += Time.deltaTime;
            if(timeSinceLastChange >= healthChangeDelay)
            {
                OnInvincibilityEnd?.Invoke();
                isAttacked = false;
            }
        }
        
    }

    public bool ChangeHealth(float amount)
    {
        if (timeSinceLastChange < healthChangeDelay)
        {
            return false;
        }

        timeSinceLastChange = 0f;
        CurrentHealth += amount;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);

        if (CurrentHealth <= 0f)
        {
            CallDeath(transform.position);
            return true;
        }
        if (amount > 0)
        {
            OnHeal?.Invoke();
        }
        else
        {
            isAttacked = true;
            OnDamage?.Invoke();
        }
        return true;
    }

    private void CallDeath(Vector2 position)
    {
        OnDeath?.Invoke(position);
    }

    public void ResetHealth()
    {
        CurrentHealth = MaxHealth;
        isAttacked = false;
        timeSinceLastChange = float.MaxValue;
    }
}

