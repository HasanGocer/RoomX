using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractiveManager : MonoSingleton<InteractiveManager>
{
    [SerializeField] UnityEngine.Animations.Rigging.ChainIKConstraint chainIK;
    bool pickUpBool = false;

    public void ChainIKOff() { pickUpBool = true; }
    public void ChainIKOn() { chainIK.weight = 1; }

    private void Update()
    {
        if (pickUpBool && chainIK.weight != 0)
        {
            chainIK.weight = 0;
            if (chainIK.weight == 0)
                pickUpBool = false;
        }
    }

    public void PerspectiveCameraOn()
    {
        CameraTargetFollow.Instance.enabled = true;
        CharacterMove.Instance.enabled = true;
    }

    public void PerspectiveCameraOff()
    {
        CameraTargetFollow.Instance.enabled = false;
        CharacterMove.Instance.enabled = false;
    }
}
