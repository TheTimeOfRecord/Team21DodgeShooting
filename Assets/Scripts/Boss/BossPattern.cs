using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPattern : MonoBehaviour
{
    [SerializeField] private Transform[] WeaponPivots;      //1, 3, 4, 0순서, 5,6은 날개 끝자락
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

    private void FirstPattern()     //총알세례. 4개의 정면피벗에서 일직선 총알 다수 발사
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

    private void SecondPattern()    //1패턴과함께 유도탄추가
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


    private void ThirdPattern()     //원형으로 마구마구 퍼지는 패턴
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


    private void FourthPattern()      //화면 밖에서 총알 발사
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

    private void FifthPattern()     //화면밖에서 몬스터 소환과 동시에 탄환발사
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
        //부채꼴 모양의 일반총알 bulletNumber만큼 발사
    }

    private void HomingMissile(float bulletNumber)
    {
        //유도탄 발사
    }

    private void SpreadBullets(float bulletNumber)
    {
        //원형탄막 발사
    }

    private void razor(float bulletNumber)
    {
        //PierceBullet number만큼 발사
    }
}
