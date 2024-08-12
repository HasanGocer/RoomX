using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractWithObject : MonoBehaviour
{
    [SerializeField] Button interactiveButton;
    [SerializeField] KeyCode interactiveKey = KeyCode.E;
    [SerializeField] string interactiveName;
    [SerializeField] string paintingName;
    [SerializeField] string DoorName;

    GameObject triggerObject;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(interactiveName) || other.CompareTag(paintingName) || other.CompareTag(DoorName))
        {
            interactiveButton.gameObject.SetActive(true);
            triggerObject = other.gameObject;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (triggerObject == other.gameObject)
        {
            triggerObject = null;
            interactiveButton.gameObject.SetActive(false);
        }
    }

    private void Start()
    {
        interactiveButton.onClick.AddListener(InteractiveStart);
    }
    private void Update()
    {
        if (Input.GetKeyDown(interactiveKey) && triggerObject != null) InteractiveStart();
    }


    void InteractiveStart()
    {
        if (triggerObject.CompareTag(interactiveName))
        {
            PickUpSystem.Instance.PickUpStart(triggerObject);
            ÝnterctiveOff();
        }
        else if (triggerObject.CompareTag(paintingName))
        {
            PaintingSearchSystem.Instance.StartPaintingSearch(triggerObject);
            ÝnterctiveOff();
        }
        else if (triggerObject.CompareTag(DoorName))
        {
            DoorManager.Instance.StartDoorIn(gameObject, triggerObject);
            ÝnterctiveOff();
        }
    }
    private void ÝnterctiveOff()
    {
        interactiveButton.gameObject.SetActive(false);
        triggerObject = null;
    }
}
