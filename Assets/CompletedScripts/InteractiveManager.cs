using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractiveManager : MonoSingleton<InteractiveManager>
{
    [SerializeField] List<GameObject> panels = new List<GameObject>();
    [SerializeField] GameObject _camera, cameraPos, targetPos;
    [SerializeField] Button closeButton;
    GameObject target;
    private void Start()
    {
        closeButton.onClick.AddListener(PerspectiveOff);
    }

    public void SetTarget(GameObject tempTarget)
    {
        target = tempTarget;
    }

    public void GetTarget()
    {
        InteractiveID interactiveID = target.GetComponent<InteractiveID>();
        bool tempBool = true;

        interactiveID.SetScale();
        MoveMechanics.Instance.MoveLerp(target, targetPos.transform.position, 1, ref tempBool);
        MoveMechanics.Instance.MoveLerpQuaternion(_camera, cameraPos.transform.rotation, 1, ref tempBool);
        MoveMechanics.Instance.MoveLerp(_camera, cameraPos.transform.position, 1, ref tempBool);
        interactiveID.GetObjectRotation().enabled = true;
    }

    public void PerspectiveOff()
    {
        InteractiveID interactiveID = target.GetComponent<InteractiveID>();

        EnvanterManager.Instance.ItemAdd(interactiveID);
        CameraTargetFollow.Instance.enabled = true;
        CharacterMove.Instance.enabled = true;
        interactiveID.GetObjectRotation().enabled = false;
        closeButton.gameObject.SetActive(false);
        target.SetActive(false);
    }

    public void PerspectiveOn()
    {
        CameraTargetFollow.Instance.enabled = false;
        CharacterMove.Instance.enabled = false;
        closeButton.gameObject.SetActive(true);
    }

    public void PanelOpen(int panelCount)
    {
        panels[panelCount].SetActive(true);
    }
}
