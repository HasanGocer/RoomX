using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class PickUpSystem : MonoSingleton<PickUpSystem>
{
    [SerializeField] GameObject character, _camera, cameraPos;
    [SerializeField] Button closeButton;
    GameObject pickUpItem;

    public void PickUpStart(GameObject tempTarget)
    {
        closeButton.onClick.AddListener(AddItem);
        InteractiveManager.Instance.PerspectiveCameraOff();
        CharacterMove.Instance.NavmeshAgentOff();
        ItemAddSystem.Instance.enabled = true;
        pickUpItem = tempTarget;
        PickUpSystem.Instance.PickUpFinish();
        CharacterMove.Instance.gameObject.SetActive(false);
    }

    public void PickUpFinish()
    {
        InteractiveID interactiveID = pickUpItem.GetComponent<InteractiveID>();

        closeButton.gameObject.SetActive(true);
        // interactiveID.GetObjectRotation().enabled = true;

        // interactiveID.SetScale();
        CharacterMove.Instance.NavmeshAgentOn();

        character.transform.LookAt(pickUpItem.transform);
        _camera.transform.position = cameraPos.transform.position;
        _camera.transform.rotation = cameraPos.transform.rotation;
    }
    private void AddItem()
    {
        InteractiveManager.Instance.PerspectiveCameraOn();
        ItemAddSystem.Instance.enabled = false;

        closeButton.gameObject.SetActive(false);
        CharacterMove.Instance.gameObject.SetActive(true);
    }
}
