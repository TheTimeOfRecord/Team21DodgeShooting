using System;
using UnityEngine;

//관통형 투사체. 대상에게 부딪혀도 사라지지 않고 계속 움직임
public class PierceBullet : StandardBullet
{
    public override void OnImpact(Collider2D collision)
    {
        //데미지 처리는 하되 파괴되지는 않는
        HealthSystem healthSystem = collision.gameObject.GetComponent<HealthSystem>();
        StatHandler statHandler = collision.GetComponent<StatHandler>();
        healthSystem.ChangeHealth(statHandler.CurrentStat.ATK * -1);
    }

}
