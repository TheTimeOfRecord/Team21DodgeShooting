using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class BossControllerTest : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Button RetryBtn;
    [SerializeField] private TMP_Text killtext;

    private Vector3 screenOffset = new Vector3(0.5f, 0.5f, 10);

    private Animator animCtrl;

    private void Awake()
    {
        animCtrl = GetComponentInChildren<Animator>();
        Invoke("OnBossLoadEnd", 3);
        RetryBtn.onClick.AddListener(Retry);
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
        FollowPlayer();
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
}
