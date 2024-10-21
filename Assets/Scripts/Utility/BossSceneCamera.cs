using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossSceneCamera : MonoBehaviour
{
    private Animator animCrtl;

    private void Awake()
    {
        animCrtl = GetComponent<Animator>();
    }

    private void Start()
    {
        animCrtl.SetTrigger("OnCameraSet");
    }
}