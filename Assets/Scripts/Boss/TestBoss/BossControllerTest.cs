using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Random = UnityEngine.Random;
using Unity.VisualScripting;

public class BossControllerTest : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Button RetryBtn;
    [SerializeField] private TMP_Text killtext;

    private Vector3 screenOffset = new Vector3(0.5f, 0.5f, 10);
    private int pattern = 5;

    private Animator animCtrl;
    private BossPatternTest bossPattern;
    private HealthSystem healthSystem;
    private StatHandler statHandler;
    private Rigidbody2D rb;

    [SerializeField] private float blinkDelayTime = 1f;
    private bool canBlink;
    private Vector2 direction;
    private float distance;

    [SerializeField, Range(1f, 5f)] private float Range = 5f;
    private float rad;
    private bool isRanged;
    private Vector2 initDirectionInRange;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        healthSystem = GetComponent<HealthSystem>();
        statHandler = GetComponent<StatHandler>();
        animCtrl = GetComponentInChildren<Animator>();
        Invoke("OnBossLoadEnd", 3);
        RetryBtn.onClick.AddListener(Retry);
        bossPattern = GetComponentInChildren<BossPatternTest>();
    }

    private void Start()
    {
        bossPattern.PatternEvent += ChangePatternIndex;
    }

    private void ChangePatternIndex(int patternIndex)
    {
        pattern = patternIndex;
    }

    private void BossMove(int patternIndex)
    {
        switch (patternIndex)
        {
            case 0:
                Hovering();
                break;
            case 1:
                Hovering();
                break;
            case 2:
                ChargingMove();
                break;
            case 3:
                BlinkingMove();
                break;
            case 4:
                DefaultMove();
                break;
            default:        //패턴 진행중이 아닐경우
                DefaultMove();
                break;
        }

    }

    private void Update()
    {
        killtext.text = $"You Killed {GameManager.Instance.EnemyDeathCount}";
    }

    private void OnBossLoadEnd()
    {
        animCtrl.SetBool("isEndLoad", true);
    }

    private void Retry()
    {
        SceneManager.LoadScene("PlayerChooseScene");
    }


    private void FixedUpdate()
    {
        if(animCtrl.GetBool("isEndLoad"))
        RotateToTarget(direction);
        BossMove(pattern);
    }

    private void FollowPlayer()
    {
        if(mainCamera != null)
        {
            Vector3 screenPosition = new Vector3(Screen.width * screenOffset.x, Screen.height * screenOffset.y, screenOffset.z);
            Vector3 worldPosition = mainCamera.ScreenToWorldPoint(screenPosition);
            worldPosition.z = 0;
            worldPosition.y -= 3;
            transform.position = worldPosition;
        }
    }

    protected float DistanceToTarget()
    {
        return Vector3.Distance(transform.position, GameManager.Instance.Player.position);
    }

    protected Vector2 DirectionToTarget()
    {
        return (GameManager.Instance.Player.position - transform.position).normalized;
    }

    protected void RotateToTarget(Vector2 _direction)
    {
        float rotZ = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotZ - 90f);
    }

    private void DefaultMove()
    {
        distance = DistanceToTarget();
        direction = DirectionToTarget();

        //플레이어 방향으로 다가가되 일정거리 이하가 되면 속도가 늦춰짐
        if(distance < 5f)
        {
            statHandler.ChangeCharacterStat(stats.speed, 3f);
        }
        else
        {
            statHandler.ChangeCharacterStat(stats.speed, 8f);
        }
        rb.velocity = direction * statHandler.CurrentStat.speed;
    }

    private void TractingMove()
    {
        statHandler.ChangeCharacterStat(stats.speed, 5f);
        direction = DirectionToTarget();
        distance = DistanceToTarget();
        if(distance < 5)
        {
            rb.velocity = direction * 3;
        }
        rb.velocity = direction * statHandler.CurrentStat.speed;
    }

    private void BlinkingMove()
    {
        direction = DirectionToTarget();
        if (canBlink)
        {
            canBlink = false;
            StartCoroutine(BlinkCoroutine());
        }
    }

    IEnumerator BlinkCoroutine()
    {
        Blink();
        yield return new WaitForSeconds(blinkDelayTime);
        canBlink = true;
    }

    private void Blink()
    {
        float x = Random.Range(-5f, 5f);
        float y = Random.Range(-5f, 5f);

        if ((x > -2 && x < 2) && (y > -2 && y < 2))
        {
            x = 3f;
            y = 3f;
        }

        transform.position = GameManager.Instance.Player.position + new Vector3(x, y, 0f);
    }

    private void HoveringMove()
    {
        direction = DirectionToTarget();

        if (!isRanged)
        {
            float distance = DistanceToTarget();

            if (distance <= Range)
            {
                isRanged = true;
                initDirectionInRange = direction * -1;
            }
        }
        else
        {
            direction = direction * -1;
            float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.localRotation = Quaternion.Euler(0, 0, rotZ);
            Hovering();
        }
    }

    private void Hovering()
    {
        rad += Time.deltaTime * (statHandler.CurrentStat.speed / Range);
        float initialRad = Mathf.Atan2(initDirectionInRange.y, initDirectionInRange.x);
        float x = Range * Mathf.Cos(rad + initialRad);
        float y = Range * Mathf.Sin(rad + initialRad);
        transform.position = GameManager.Instance.Player.position + new Vector3(x, y);
        if (rad >= Mathf.PI * 2) rad = 0;
    }

    private void ChargingMove()
    {
        direction = DirectionToTarget();

        StartCoroutine(Charging());
    }

    private IEnumerator Charging()
    {
        statHandler.ChangeCharacterStat(stats.speed, 0f);
        RotateToTarget(direction);
        yield return waitFiveSeconds;

        Vector2 lastPos = DirectionToTarget();
        statHandler.ChangeCharacterStat(stats.speed, 10f);
        rb.velocity = lastPos * statHandler.CurrentStat.speed;

        if((Vector2)transform.position == lastPos)
        {
            statHandler.ChangeCharacterStat(stats.speed, 5f);
        }
    }

    WaitForSeconds waitFiveSeconds = new WaitForSeconds(5f);
}
