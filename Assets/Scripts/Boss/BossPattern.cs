using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPattern : MonoBehaviour
{
    [SerializeField] private Transform[] WeaponPivots;      //1, 3, 4, 0����, 5,6�� ���� ���ڶ�
    [SerializeField] private Transform[] OutsidePositions;

    private HealthSystem healthSystem;
    private StatHandler statHandler;
    private Animator anim;

    private bool isAlive = true;
    private int patternIndex;
    private int currentPatternCount;
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
        if (!isAlive)
        {
            return;
        }
        patternIndex = patternIndex == 4 ? 0 : patternIndex++;
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


        currentPatternCount++;
        if(currentPatternCount < maxPatternCount[patternIndex])
        {
            Invoke("FirstPattern", 2);
        }
        else
        {
            Invoke("Think", 3f);
        }
    }

    private void SecondPattern()    //1���ϰ��Բ� ����ź�߰�
    {
        currentPatternCount++;
        if (currentPatternCount < maxPatternCount[patternIndex])
        {
            Invoke("SecondPattern", 2);
        }
        else
        {
            Invoke("Think", 3f);
        }
    }


    private void ThirdPattern()     //�������� �������� ������ ����
    {
        currentPatternCount++;
        if (currentPatternCount < maxPatternCount[patternIndex])
        {
            Invoke("ThirdPattern", 2);
        }
        else
        {
            Invoke("Think", 3f);
        }
    }


    private void FourthPattern()      //ȭ�� �ۿ��� �Ѿ� �߻�
    {
        currentPatternCount++;
        if (currentPatternCount < maxPatternCount[patternIndex])
        {
            Invoke("FourthPattern", 2);
        }
        else
        {
            Invoke("Think", 3f);
        }
    }

    private void FifthPattern()     //ȭ��ۿ��� ���� ��ȯ�� ���ÿ� źȯ�߻�
    {
        currentPatternCount++;
        if (currentPatternCount < maxPatternCount[patternIndex])
        {
            Invoke("FifthPattern", 2);
        }
        else
        {
            Invoke("Think", 3f);
        }
    }

    private void Shotgun(float bulletNumber)
    {
        //��ä�� ����� �Ϲ��Ѿ� bulletNumber��ŭ �߻�
    }

    private void HomingMissile(float bulletNumber)
    {
        //����ź �߻�
    }

    private void SpreadBullets(float bulletNumber)
    {
        //����ź�� �߻�
    }

    private void razor(float bulletNumber)
    {
        //PierceBullet number��ŭ �߻�
    }
}
