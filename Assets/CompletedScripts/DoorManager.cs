using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoSingleton<DoorManager>
{
    [SerializeField] AnimationClip doorClip;


    public float GetDoorClipTime() { return doorClip.length; }

    public void StartDoorIn(GameObject character, GameObject door)
    {
        DoorController doorController = door.GetComponent<DoorController>();
        bool tempBool = true;

        InteractiveManager.Instance.PerspectiveCameraOff();

        MoveMechanics.Instance.MoveLerpLocalQuaternion(character, doorController.GetDoorCharacterPos().transform.localRotation, 3, ref tempBool);
        MoveMechanics.Instance.MoveStabile(character, doorController.GetDoorCharacterPos().transform.position, 1, ref tempBool, () =>
             {
                 InteractiveManager.Instance.ChainIKOn();
                 CharacterMove.Instance.NavmeshAgentOff();
                 CharacterAnim.Instance.PickUpTargetMove(doorController.GetDoorHandPos());
                 CharacterAnim.Instance.DoorInAnim();
             });
    }

    public void FinishDoorClipTime()
    {
        CharacterAnim.Instance.ResetPickUpTarget();
        InteractiveManager.Instance.PerspectiveCameraOn();
        InteractiveManager.Instance.ChainIKOff();
        CharacterMove.Instance.NavmeshAgentOn();



    }
}
