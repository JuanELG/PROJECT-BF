using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetBoolAnimation(PlayerAnimations animationId, bool state)
    {
        _animator.SetBool(animationId.ToString(), state);
    }
}

public enum PlayerAnimations
{
    IsWalking
}
