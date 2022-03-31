using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    private Animator _animator;

    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void AnimationWalking(bool walkingLeft)
    {
        if (walkingLeft)
            _animator.SetBool(AnimationStates.WalkingLeft.ToString(), true);
        if (!walkingLeft)
            _animator.SetBool(AnimationStates.WalkingRight.ToString(), true);
    }

    public void AnimationJumping()
    {
        _animator.SetTrigger(AnimationStates.Jumping.ToString());
    }

    private enum AnimationStates
    {
        WalkingRight,
        WalkingLeft,
        Jumping,
        Attack
    }
}