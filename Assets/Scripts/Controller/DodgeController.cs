using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeController : MonoBehaviour
{
    public event Action<Vector2> OnMoveEvent;
    public event Action<Vector2> OnLookEvent;
    public event Action OnFireEvent;       //<> 추후 수정 예정

    private float timeSinceLastAttack = float.MaxValue;
    protected bool isAttacking;

    protected StatHandler stats { get; private set; }

    protected virtual void Awake()
    {
        stats = GetComponent<StatHandler>();
    }

    protected void Update()
    {
        HandleAttackDelay();
    }

    private void HandleAttackDelay()
    {
        if(timeSinceLastAttack <= stats.CurrentStat.delay)
        {
            timeSinceLastAttack += Time.deltaTime;
        }

        if(isAttacking && timeSinceLastAttack > stats.CurrentStat.delay)
        {
            timeSinceLastAttack = 0;
            CallFireEvent();
        }
    }

    public void CallMoveEvent(Vector2 direction)
    {
        OnMoveEvent?.Invoke(direction);
    }

    public void CallLookEvent(Vector2 direction)
    {
        OnLookEvent?.Invoke(direction);
    }

    public void CallFireEvent()
    {
        OnFireEvent?.Invoke();
    }
}
