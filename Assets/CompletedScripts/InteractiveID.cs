using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveID : MonoBehaviour
{
    [SerializeField] int interactiveID;
    [SerializeField] string interactiveName;
    [SerializeField] ObjectRotation objectRotation;
    [SerializeField] GameObject objectHandIKPos;
    [SerializeField] Vector3 tempMiniScale;

    public int GetInteractiveID() { return interactiveID; }
    public GameObject GetObjectHandIKPos() { return objectHandIKPos; }
    public string GetInteractiveName() { return interactiveName; }
    public ObjectRotation GetObjectRotation() { return objectRotation; }
    public void SetScale() { transform.localScale = tempMiniScale; }


    private void Start()
    {
        CheckedObjectActive();
    }
    public void ItemAdd()
    {

    }

    private void CheckedObjectActive()
    {
        if (EnvanterManager.Instance.envanter.itemChecked[interactiveID]) gameObject.SetActive(false);
    }
}
