using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUpSystem : MonoSingleton<PickUpSystem>
{
    [SerializeField] GameObject _camera, cameraPos, targetPos;
    [SerializeField] Button closeButton;
    GameObject pickUpItem;

    private void Start()
    {
        closeButton.onClick.AddListener(AddItem);
    }

    public void PickUpStart(GameObject tempTarget)
    {
        CharacterAnim.Instance.PickUpAnim();
        InteractiveManager.Instance.ChainIKOn();
        InteractiveManager.Instance.PerspectiveCameraOff();
        CharacterAnim.Instance.PickUpTargetMove(tempTarget);
        CharacterMove.Instance.NavmeshAgentOff();
        pickUpItem = tempTarget;
    }

    public void PickUpFinish()
    {
        InteractiveID interactiveID = pickUpItem.GetComponent<InteractiveID>();
        bool tempBool = true;

        closeButton.gameObject.SetActive(true);
        interactiveID.GetObjectRotation().enabled = true;

        interactiveID.SetScale();
        CharacterMove.Instance.NavmeshAgentOn();
        InteractiveManager.Instance.ChainIKOff();
        CharacterAnim.Instance.ResetPickUpTarget();

        MoveMechanics.Instance.MoveLerp(pickUpItem, targetPos.transform.position, 1, ref tempBool);
        MoveMechanics.Instance.MoveLerp(_camera, cameraPos.transform.position, 1, ref tempBool);
        MoveMechanics.Instance.MoveLerpQuaternion(_camera, cameraPos.transform.rotation, 1, ref tempBool);
    }
    private void AddItem()
    {
        InteractiveID interactiveID = pickUpItem.GetComponent<InteractiveID>();

        EnvanterManager.Instance.ItemAdd(interactiveID);
        InteractiveManager.Instance.PerspectiveCameraOn();


        interactiveID.GetObjectRotation().enabled = false;
        closeButton.gameObject.SetActive(false);
        pickUpItem.SetActive(false);
    }
}
