using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInScript : StateMachineBehaviour
{
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        DoorManager.Instance.FinishDoorClipTime();
    }
}
