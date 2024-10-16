using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimController : MonoBehaviour
{
    private DodgeController controller;

    private void Awake()
    {
        controller = GetComponent<DodgeController>();
    }

    private void Start()
    {
        controller.OnLookEvent += OnAim;
    }

    private void OnAim(Vector2 vector)
    {
        RotateWeapon(vector);
    }

    private void RotateWeapon(Vector2 vector)
    {
        float rotZ = Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg;
    }
}
