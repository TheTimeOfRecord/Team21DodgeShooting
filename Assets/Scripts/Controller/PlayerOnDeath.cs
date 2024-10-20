using System;
using UnityEngine;
using UnityEngine.UI;
public class PlayerOnDeath : MonoBehaviour
{
    //Á×¾úÀ» ¶§ÀÇ UI ¿ÀÇÂ

    private HealthSystem healthSystem;
    private Animator anim;

    private void Awake()
    {
        healthSystem = GetComponent<HealthSystem>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        healthSystem.OnDeath += PlayerDead;
    }

    private void PlayerDead()
    {
        anim.SetTrigger("OnDeath");
    }
}