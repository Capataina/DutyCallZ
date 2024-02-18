using UnityEngine;

public static class AnimationTools
{
    public static void CrossFadeToAnimationFT(Animator animator, string stateName, float transitionTime, int layer)
    {
        if (!animator.GetCurrentAnimatorStateInfo(layer).IsName(stateName)
        && !animator.GetNextAnimatorStateInfo(layer).IsName(stateName))
        {
            animator.CrossFadeInFixedTime(stateName, transitionTime, layer);
        }
    }

    public static bool AnimationIsActiveAndFinished(Animator animator, string stateName, int layer)
    {
        return animator.GetCurrentAnimatorStateInfo(layer).normalizedTime >= 1.0f &&
                    animator.GetCurrentAnimatorStateInfo(layer).IsName(stateName);
    }
}