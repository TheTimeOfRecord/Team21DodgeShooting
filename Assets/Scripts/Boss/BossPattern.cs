using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class BossPattern : MonoBehaviour
{
    [SerializeField] private Transform[] WeaponPivots;      //1, 3, 2, 0����, 5,4�� ���� ���ڶ�
    [SerializeField] private Transform[] OutsidePositions;

    private HealthSystem healthSystem;
    private StatHandler statHandler;
    private Animator anim;

    private bool isAlive = true;
    private int patternIndex = 0;
    private int currentPatternCount = 0;
    public int[] maxPatternCount;

    private void Awake()
    {
        healthSystem = GetComponent<HealthSystem>();
        statHandler = GetComponent<StatHandler>();
        anim = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        healthSystem.OnDeath -= OnDead;
        healthSystem.OnDeath += OnDead;
        Invoke("Think", 5f);
    }

    private void OnDead()
    {
        isAlive = false;
        anim.SetTrigger("OnBossDead");
        Invoke("DisappearBoss", 4.2f);
    }

    private void DisappearBoss()
    {
        gameObject.SetActive(false);
    }

    private void Think()
    {
        //Debug.Log($"������..." +
        //    $"�������� : {patternIndex}" +
        //    $"���� ���� �ݺ� : {currentPatternCount}" +
        //    $"���� ������ �ƽ�ī��Ʈ : {maxPatternCount[patternIndex]}");
        if (!isAlive)
        {
            return;
        }
        currentPatternCount = 0;

        switch (patternIndex)
        {
            case 0:
                FirstPattern();
                break;

            case 1:
                SecondPattern();
                break;

            case 2:
                ThirdPattern();
                break;

            case 3:
                FourthPattern();
                break;

            case 4:
                FifthPattern();
                break;

        }

    }

    private void FirstPattern()     //�Ѿ˼���. 4���� �����ǹ����� ������ �Ѿ� �ټ� �߻�
    {
        Shotgun(20);

        currentPatternCount++;
        if(currentPatternCount < maxPatternCount[patternIndex])
        {
            Invoke("FirstPattern", 1);
        }
        else
        {
            patternIndex++;

            Invoke("Think", 3f);
        }
    }

    private void SecondPattern()    //1���ϰ��Բ� ����ź�߰�
    {
        Shotgun(20);
        HomingMissile(2);

        currentPatternCount++;
        if (currentPatternCount < maxPatternCount[patternIndex])
        {
            Invoke("SecondPattern", 2);
        }
        else
        {
            patternIndex++;
            Invoke("Think", 3f);
        }
    }


    private void ThirdPattern()     //�������� �������� ������ ����
    {
        SpreadBullets(30);

        currentPatternCount++;
        if (currentPatternCount < maxPatternCount[patternIndex])
        {
            Invoke("ThirdPattern", 0.5f);
        }
        else
        {
            patternIndex++;
            Invoke("Think", 3f);
        }
    }


    private void FourthPattern()      //ȭ�� �ۿ��� �Ѿ� �߻�
    {
        Debug.Log("ȭ�� �ۿ��� �Ѿ� �߻��Ұǵ� �ϴ� 3�����ϰ� ����");
        SpreadBullets(30);

        currentPatternCount++;
        if (currentPatternCount < maxPatternCount[patternIndex])
        {
            Invoke("FourthPattern", 2);
        }
        else
        {
            patternIndex++;
            Invoke("Think", 3f);
        }
    }

    private void FifthPattern()     //ȭ��ۿ��� ���� ��ȯ�� ���ÿ� źȯ�߻�
    {
        Debug.Log("ȭ�� �ۿ��� ���� ��ȯ�� ���ÿ� źȯ �߻��Ұǵ� �ϴ� 3�����ϰ� ����");
        SpreadBullets(30);

        currentPatternCount++;
        if (currentPatternCount < maxPatternCount[patternIndex])
        {
            Invoke("FifthPattern", 2);
        }
        else
        {
            patternIndex = 0;
            Invoke("Think", 3f);
        }
    }

    private void Shotgun(float bulletNumber)
    {
        //��ä�� ����� �Ϲ��Ѿ� bulletNumber��ŭ �߻�
        statHandler.ChangeCharacterStat(stats.bulletNum, bulletNumber);

        for (int i = 0; i < 4; i++)
        {
            GameObject projectile = GameManager.Instance.objPool.GetObjectFromPool("StandardBullet", WeaponPivots[i].position);

            Bullet bullet = projectile.GetComponent<Bullet>();
            bullet.SetShooter(this.gameObject);

            if (bullet != null)
            {
                bullet.Move(statHandler.CurrentStat.bulletSpeed, Vector2.down);
            }
        }
    }

    private void HomingMissile(float bulletNumber)
    {
        //����ź �߻�
        statHandler.ChangeCharacterStat(stats.bulletNum, bulletNumber);
        for(int i = 0; i < 4; i++)
        {
            GameObject projectile = GameManager.Instance.objPool.GetObjectFromPool("HomingBullet", WeaponPivots[i].position);

            Bullet bullet = projectile.GetComponent<Bullet>();
            bullet.SetShooter(this.gameObject);

            if (bullet != null)
            {
                Vector2 target = GameManager.Instance.Player.position;
                bullet.SetShooter(this.gameObject);
                bullet.Move(statHandler.CurrentStat.bulletSpeed, target);
            }
        }
    }

    private void SpreadBullets(float bulletNumber)
    {
        //����ź�� �߻�
        statHandler.ChangeCharacterStat(stats.bulletNum, bulletNumber);

        for(int i = 4; i < 6; i++)
        {
            GameObject spreadBulletProjectile = GameManager.Instance.objPool.GetObjectFromPool("SpreadBullet");

            Bullet spreadBullet = spreadBulletProjectile.GetComponent<Bullet>();
            spreadBullet.SetShooter(this.gameObject);

            if (spreadBullet != null)
            {
                spreadBullet.Move(statHandler.CurrentStat.bulletSpeed, WeaponPivots[i].position);
            }
        }
    }

    private void razor(float bulletNumber)
    {
        //PierceBullet number��ŭ �߻�
        statHandler.ChangeCharacterStat(stats.bulletNum, bulletNumber);
        for (int i = 0; i < 4; i++)
        {
            GameObject projectile = GameManager.Instance.objPool.GetObjectFromPool("PierceBullet", WeaponPivots[i].position);

            Bullet bullet = projectile.GetComponent<Bullet>();
            bullet.SetShooter(this.gameObject);

            if (bullet != null)
            {
                Vector2 target = GameManager.Instance.Player.position;
                bullet.SetShooter(this.gameObject);
                bullet.Move(statHandler.CurrentStat.bulletSpeed, target);
            }
        }
    }
}
