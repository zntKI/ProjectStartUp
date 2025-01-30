using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    Animator currentAnimator;

    void Start()
    {
        UpdateAnimator();
    }

    /// <summary>
    /// Called after having switched models and other animator is now enabled instead
    /// </summary>
    public void UpdateAnimator()
    {
        currentAnimator = GetComponentsInChildren<Animator>().First(a => a.enabled);
    }

    public void PlayIdle(ItemController heldItem)
    {
        string animNameToPlay = "";
        if (heldItem == null)
            animNameToPlay = "metarig|Idle";
        else
            animNameToPlay = "metarig|Idle_ Holding";

        if (!currentAnimator.GetCurrentAnimatorStateInfo(0).IsName(animNameToPlay)) // Do not reset if already playing
        {
            currentAnimator.Play(animNameToPlay);
        }
    }

    public void PlayRun(ItemController heldItem)
    {
        string animNameToPlay = "";
        if (heldItem == null)
            animNameToPlay = "metarig|Run";
        else
            animNameToPlay = "metarig|Run Holding";

        if (!currentAnimator.GetCurrentAnimatorStateInfo(0).IsName(animNameToPlay)) // Do not reset if already playing
        {
            currentAnimator.Play(animNameToPlay);
        }
    }

    public void PlayOnItemHold()
    {
        if (!currentAnimator.GetCurrentAnimatorStateInfo(0).IsName("metarig|Idle_ Holding")) // Do not reset if already playing
        {
            currentAnimator.Play("metarig|Idle_ Holding");
        }
    }

    public void PlayOnItemDrop()
    {
        if (!currentAnimator.GetCurrentAnimatorStateInfo(0).IsName("metarig|Idle")) // Do not reset if already playing
        {
            currentAnimator.Play("metarig|Idle");
        }
    }
}
