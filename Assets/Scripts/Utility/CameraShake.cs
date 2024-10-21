using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{
    [SerializeField] GameObject playerCamera;

    CinemachineVirtualCamera vc;
    CinemachineBasicMultiChannelPerlin noise;
    Animator animCtrl;

    private void Awake()
    {
        vc = GetComponent<CinemachineVirtualCamera>();
    }

    private void Start()
    {
        animCtrl = playerCamera.GetComponent<Animator>();
        noise =vc.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        SceneManager.sceneLoaded += OnSceneLoaded;
        ShakeCamera(4, 1.5f);
        animCtrl.SetTrigger("OnCameraSet");
        Invoke("StopCameraShaking", 3f);

    }


    public void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        if(scene.name == "BossScene")
        {
            ShakeCamera(4, 1.5f);
            animCtrl.SetTrigger("OnCameraSet");
            Invoke("StopCameraShaking", 3f);
        }
    }

    public void ShakeCamera(float Amplitude, float Frequency)
    {
        noise.m_AmplitudeGain = Amplitude;
        noise.m_FrequencyGain = Frequency;
    }


    public void StopCameraShaking()
    {
        noise.m_AmplitudeGain = 0;
        noise.m_FrequencyGain = 0;
    }


}
