using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonUtilities : MonoBehaviour
{
    public static AnimationClip FindAnimation(Animator animator, string name)
    {
        foreach (AnimationClip clip in animator.runtimeAnimatorController.animationClips)
        {
            if (clip.name == name)
            {
                return clip;
            }
        }

        return null;
    }
}
