using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class BossPatternTest : MonoBehaviour
{
    [SerializeField] private Transform[] WeaponPivots;      //1, 3, 2, 0순서, 5,4은 날개 끝자락
    [SerializeField] private Transform[] OutsidePositions;
    [SerializeField] private InputActionAsset inputAsset;
    [SerializeField] private GameObject clearUICanvas;

    private HealthSystem healthSystem;
    private StatHandler statHandler;
    private Animator anim;
    private InputActionMap player01;
    private InputAction moveAction;
    private InputAction fireAction;

    private bool isAlive = true;
    private int patternIndex = 0;
    private int currentPatternCount = 0;
    public int[] maxPatternCount;

    private void Awake()
    {
        player01 = inputAsset.FindActionMap("Player01");
        moveAction = player01.FindAction("Move");
        fireAction = player01.FindAction("Fire");

        healthSystem = GetComponent<HealthSystem>();
        statHandler = GetComponent<StatHandler>();
        anim = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        healthSystem.OnDeath -= OnDead;
        healthSystem.OnDeath += OnDead;

        fireAction.Disable();
        moveAction.Disable();
        Invoke("PlayerActionEnable", 3f);
        Invoke("Think", 5f);
    }

    private void PlayerActionEnable()
    {
        moveAction.Enable();
        fireAction.Enable();
    }

    private void OnDead(Vector2 position)
    {
        isAlive = false;
        anim.SetTrigger("OnBossDead");
        moveAction.Disable();
        fireAction.Disable();
        Invoke("DisappearBoss", 4.2f);
    }

    private void DisappearBoss()
    {
        gameObject.SetActive(false);
        clearUICanvas.SetActive(true);
    }

    private void Think()
    {
        //Debug.Log($"생각중..." +
        //    $"패턴종류 : {patternIndex}" +
        //    $"현재 패턴 반복 : {currentPatternCount}" +
        //    $"현재 패턴의 맥스카운트 : {maxPatternCount[patternIndex]}");
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

    private void FirstPattern()     //총알세례. 4개의 정면피벗에서 일직선 총알 다수 발사
    {
        Shotgun(20);

        currentPatternCount++;
        if (currentPatternCount < maxPatternCount[patternIndex])
        {
            Invoke("FirstPattern", 1);
        }
        else
        {
            patternIndex++;

            Invoke("Think", 3f);
        }
    }

    private void SecondPattern()    //1패턴과함께 유도탄추가
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


    private void ThirdPattern()     //원형으로 마구마구 퍼지는 패턴
    {
        StartCoroutine(SpreadBullets(30));

        currentPatternCount++;
        if (currentPatternCount < maxPatternCount[patternIndex])
        {
            Invoke("ThirdPattern", 1f);
        }
        else
        {
            patternIndex++;
            Invoke("Think", 5f);
        }
    }


    private void FourthPattern()      //화면 밖에서 총알 발사
    {
        for (int i = 0; i < OutsidePositions.Length; i++)
        {
            StartCoroutine(Razor(5));
        }


        currentPatternCount++;
        if (currentPatternCount < maxPatternCount[patternIndex])
        {
            Invoke("FourthPattern", 5);
        }
        else
        {
            patternIndex++;
            Invoke("Think", 5f);
        }
    }

    private void FifthPattern()     //화면밖에서 몬스터 소환과 동시에 탄환발사
    {
        Debug.Log("화면 밖에서 몬스터 소환과 동시에 탄환 발사할건데 일단 3번패턴과 동일");
        for (int i = 0; i < OutsidePositions.Length; i++)
        {
            StartCoroutine(SummonAllMonsters());
            StartCoroutine(Razor(5));
            SpreadBullets(50);
        }

        currentPatternCount++;
        if (currentPatternCount < maxPatternCount[patternIndex])
        {
            Invoke("FifthPattern", 20);
        }
        else
        {
            patternIndex = 0;
            Invoke("Think", 5f);
        }
    }


    private void Shotgun(float bulletNumber)
    {
        //부채꼴 모양의 일반총알 bulletNumber만큼 발사
        statHandler.ChangeCharacterStat(stats.bulletNum, bulletNumber);

        for (int i = 0; i < 4; i++)
        {
            GameObject projectile = GameManager.Instance.objPool.GetObjectFromPool("StandardBullet", WeaponPivots[i].position);

            Bullet bullet = projectile.GetComponent<Bullet>();
            bullet.SetShooter(this.gameObject);

            if (bullet != null)
            {
                bullet.Move(statHandler.CurrentStat.bulletSpeed, GameManager.Instance.Player.position);
            }
        }
    }

    private void HomingMissile(float bulletNumber)
    {
        //유도탄 발사
        statHandler.ChangeCharacterStat(stats.bulletNum, bulletNumber);
        for (int i = 0; i < 4; i++)
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

    private IEnumerator SpreadBullets(float bulletNumber)
    {
        //원형탄막 발사
        statHandler.ChangeCharacterStat(stats.bulletNum, bulletNumber);

        for (int i = 4; i < 6; i++)
        {
            GameObject spreadBulletProjectile = GameManager.Instance.objPool.GetObjectFromPool("SpreadBullet");

            Bullet spreadBullet = spreadBulletProjectile.GetComponent<Bullet>();
            spreadBullet.SetShooter(this.gameObject);

            if (spreadBullet != null)
            {
                spreadBullet.Move(statHandler.CurrentStat.bulletSpeed, WeaponPivots[i].position);
            }

            yield return null;
        }
    }

    private IEnumerator Razor(float bulletNumber)
    {
        //PierceBullet number만큼 발사
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
                bullet.Move(statHandler.CurrentStat.bulletSpeed, WeaponPivots[i].position + Vector3.down * 1000);
            }
            yield return wait;
        }
    }

    private IEnumerator SummonAllMonsters()
    {
        for (int i = 0; i < OutsidePositions.Length; i++)
        {
            string tag = Enum.GetName(typeof(EnemyType), i % 4).ToString();
            GameObject monster = GameManager.Instance.objPool.GetObjectFromPool(tag, OutsidePositions[i].position);
            monster.SetActive(true);

            yield return wait;
        }

    }


    WaitForSeconds wait = new WaitForSeconds(0.5f);

}
