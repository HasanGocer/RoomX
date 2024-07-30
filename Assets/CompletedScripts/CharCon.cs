using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharCon : MonoSingleton<CharCon>
{
    [SerializeField] Animator animator;
    [SerializeField] Transform fulcrum;

    float leftWarning = 0, rightWarning = 0;
    RaycastHit lastLeftWarning, lastRightWarning;

    private void Start()
    {
        fulcrum.parent = null;
        animator.SetLayerWeight(1, 1);
        animator.SetLayerWeight(2, 1);
    }

    public void IK()
    {

    }
}
