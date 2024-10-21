using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public RuntimeAnimatorController[] Controllers;
    public Animator Anim;

    private void Awake()
    {
        Anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        CharacterAnimation(GameManager.Instance.PlayerId);
    }

    public void CharacterAnimation(int playerId)
    {
        Anim.runtimeAnimatorController = Controllers[playerId];
    }
}
