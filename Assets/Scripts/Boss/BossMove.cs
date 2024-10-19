using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMove : MonoBehaviour
{
    [SerializeField] private Transform playerCamera;

    private void Update()
    {
        this.transform.position = playerCamera.transform.position;
    }


}
