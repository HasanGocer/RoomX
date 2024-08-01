using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveID : MonoBehaviour
{
    [SerializeField] int interactiveID;
    [SerializeField] ObjectRotation objectRotation;
    [SerializeField] Vector3 tempMiniScale;

    private void Start()
    {
        CheckedOnline();
    }


    public void SetScale()
    {
        transform.localScale = tempMiniScale;
    }

    public int GetInteractiveID()
    {
        return interactiveID;
    }

    public ObjectRotation GetObjectRotation()
    {
        return objectRotation;
    }
    public void TouchObject()
    {
        InteractiveManager.Instance.PanelOpen(interactiveID);
    }

    private void CheckedOnline()
    {
        if (EnvanterManager.Instance.envanter.itemChecked[interactiveID]) gameObject.SetActive(false);
    }
}
