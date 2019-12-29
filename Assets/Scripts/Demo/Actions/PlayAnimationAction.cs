using UnityEngine;
using System.Collections;
using BT;

public class PlayAnimationAction : BTAction
{
    private string _animationName;


    public PlayAnimationAction(string animationName)
    {
        _animationName = animationName;
    }

    protected override void Enter()
    {
        Animator animator = storyBoard.GetComponent<Animator>();
        animator.Play(_animationName);
    }
}
