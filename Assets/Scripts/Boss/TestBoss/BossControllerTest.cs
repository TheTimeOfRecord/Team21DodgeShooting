using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Random = UnityEngine.Random;
using Unity.VisualScripting;

//리팩토링 필요
//전반적인 애니메이션 관련 움직임, 죽을때의 모습등을 관리하는 Controller
//움직임 함수가 포함된 스크립트
//발사 로직을 포함한 스크립트
//패턴별로 어떤움직임, 투사체 발사를 취할건지에 대한 스크립트
public class BossControllerTest : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Button RetryBtn;
    [SerializeField] private TMP_Text killtext;

    private Vector3 screenOffset = new Vector3(0.5f, 0.5f, 10);
    private int pattern = 5;
    private bool isOpening = true;

    private Animator animCtrl;
    private BossPatternTest bossPattern;
    private HealthSystem healthSystem;
    private StatHandler statHandler;
    private Rigidbody2D rb;

    [SerializeField] private float blinkDelayTime = 2f;
    private bool canBlink = true;
    private bool canBossPattern = true;
    private Vector2 direction;
    private float distance;

    [SerializeField, Range(1f, 5f)] private float Range = 5f;
    private float rad;
    private bool isRanged = false;
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
        direction = DirectionToTarget();

        rb.velocity = direction * statHandler.CurrentStat.speed;

        switch (patternIndex)
        {
            case 0:
                DefaultMove();
                break;
            case 1:
                HoveringMove();
                break;
            case 2:
                BlinkingMove();
                break;
            case 3:
                ChargingMove();
                break;
            case 4:
                BlinkingMove();
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
        isOpening = false;
    }

    private void Retry()
    {
        SceneManager.LoadScene("PlayerChooseScene");
    }


    private void FixedUpdate()
    {
        if (!isOpening && bossPattern.isAlive)
        {
            if (animCtrl.GetBool("isEndLoad"))
                RotateToTarget(direction);

            direction = DirectionToTarget();
            RotateToTarget(direction);

            if (canBossPattern)
                BossMove(pattern);
        }
    }

    private void FollowPlayer()
    {
        if (mainCamera != null)
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
        transform.rotation = Quaternion.Euler(0, 0, rotZ + 90f);
    }

    private void DefaultMove()
    {
        //플레이어 방향으로 다가가되 일정거리 이하가 되면 속도가 늦춰짐
        if (distance < 5f)
        {
            statHandler.ChangeCharacterStat(stats.speed, 4f);
        }
        else
        {
            statHandler.ChangeCharacterStat(stats.speed, 12f);
        }
    }

    private void BlinkingMove()
    {
        if (canBlink)
        {
            canBossPattern = false;
            canBlink = false;
            StartCoroutine(BlinkCoroutine());
        }
    }

    IEnumerator BlinkCoroutine()
    {
        yield return null;
        Blink();
        yield return new WaitForSeconds(blinkDelayTime);
        canBlink = true;
        canBossPattern = true;
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
        Debug.Log("HoveringMove");

        direction = DirectionToTarget();

        if (!isRanged)
        {
            float distance = DistanceToTarget();
            rb.velocity = direction * statHandler.CurrentStat.speed;

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
            transform.localRotation = Quaternion.Euler(0, 0, rotZ + 180);
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
        canBossPattern = false;
        StartCoroutine(Charging());
    }

    private IEnumerator Charging()
    {
        statHandler.ChangeCharacterStat(stats.speed, 1f);
        yield return waitFiveSeconds;

        Vector2 lastPos = DirectionToTarget();
        initDirectionInRange = lastPos;
        yield return null;

        statHandler.ChangeCharacterStat(stats.speed, 10f);
        rb.velocity = initDirectionInRange * statHandler.CurrentStat.speed;
        yield return new WaitForSeconds(1f);
        rb.velocity = Vector2.zero;
        canBossPattern = true;
    }

    WaitForSeconds waitFiveSeconds = new WaitForSeconds(2f);
}
