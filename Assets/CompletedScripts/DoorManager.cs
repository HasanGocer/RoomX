using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoSingleton<DoorManager>
{
    [SerializeField] GameObject character;

    public void StartDoorIn(GameObject door)
    {
        DoorController doorController = door.GetComponent<DoorController>();
        Animation doorAnimation = door.GetComponent<Animation>();
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
                CharacterAnim.Instance.DoorInAnim();
                StartCoroutine(DoorAnim(doorAnimation, "DoorIn", doorController.GetDoorHandInCountdown()));
            });
        }
        else if (distanceToA > distanceToB)
        {
            MoveMechanics.Instance.MoveLerpLocalQuaternion(character, doorController.GetDoorCharacterOutPos().transform.localRotation, 3, ref tempBool);
            MoveMechanics.Instance.MoveStabile(character, doorController.GetDoorCharacterOutPos().transform.position, 1, ref tempBool, () =>
            {
                CharacterAnim.Instance.DoorOutAnim();
                StartCoroutine(DoorAnim(doorAnimation, "DoorOut", doorController.GetDoorHandOutCountdown()));
            });
        }


    }

    public void FinishDoorClipTime()
    {
        CharacterAnim.Instance.ResetPickUpTarget();
        InteractiveManager.Instance.PerspectiveCameraOn();
        CharacterMove.Instance.NavmeshAgentOn();
        CharacterAnim.Instance.IdleAnim();
    }

    private IEnumerator DoorAnim(Animation doorAnimation, string animName, float countdown)
    {
        yield return new WaitForSecondsRealtime(countdown);
        doorAnimation.clip = doorAnimation.GetClip(animName);
        doorAnimation.Play();
    }
}
