using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoSingleton<DoorManager>
{
    [SerializeField] AnimationClip doorClip;
    [SerializeField] GameObject character;

    public float GetDoorClipTime() { return doorClip.length; }

    public void StartDoorIn(GameObject door)
    {
        DoorController doorController = door.GetComponent<DoorController>();
        bool tempBool = true;

        InteractiveManager.Instance.PerspectiveCameraOff();
        CharacterMove.Instance.NavmeshAgentOff();
        CharacterAnim.Instance.WalkAnim();

        float distanceToA = Vector3.Distance(character.transform.position, doorController.GetDoorCharacterInPos().transform.position);

        float distanceToB = Vector3.Distance(character.transform.position, doorController.GetDoorCharacterOutPos().transform.position);

        if (distanceToA < distanceToB)
        {
            MoveMechanics.Instance.MoveLerpLocalQuaternion(character, doorController.GetDoorCharacterInPos().transform.localRotation, 3, ref tempBool);
            MoveMechanics.Instance.MoveStabile(character, doorController.GetDoorCharacterInPos().transform.position, 1, ref tempBool, () =>
            {
                InteractiveManager.Instance.ChainIKOn();
                CharacterAnim.Instance.PickUpTargetMove(doorController.GetDoorHandInPos());
                CharacterAnim.Instance.DoorInAnim();
            });
        }
        else if (distanceToA > distanceToB)
        {
            MoveMechanics.Instance.MoveLerpLocalQuaternion(character, doorController.GetDoorCharacterOutPos().transform.localRotation, 3, ref tempBool);
            MoveMechanics.Instance.MoveStabile(character, doorController.GetDoorCharacterOutPos().transform.position, 1, ref tempBool, () =>
            {
                InteractiveManager.Instance.ChainIKOn();
                CharacterAnim.Instance.PickUpTargetMove(doorController.GetDoorHandOutPos());
                CharacterAnim.Instance.DoorOutAnim();
            });
        }

        
    }

    public void FinishDoorClipTime()
    {
        CharacterAnim.Instance.ResetPickUpTarget();
        InteractiveManager.Instance.PerspectiveCameraOn();
        InteractiveManager.Instance.ChainIKOff();
        CharacterMove.Instance.NavmeshAgentOn();
        CharacterAnim.Instance.IdleAnim();
    }
}
