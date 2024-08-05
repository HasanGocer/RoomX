using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveID : MonoBehaviour
{
    [SerializeField] int interactiveID;
    [SerializeField] ObjectRotation objectRotation;
    [SerializeField] Vector3 tempMiniScale;

    public int GetInteractiveID() { return interactiveID; }
    public ObjectRotation GetObjectRotation() { return objectRotation; }
    public void SetScale() { transform.localScale = tempMiniScale; }

    private void Start()
    {
        CheckedObjectActive();
    }

    private void CheckedObjectActive()
    {
        if (EnvanterManager.Instance.envanter.itemChecked[interactiveID]) gameObject.SetActive(false);
    }
}
