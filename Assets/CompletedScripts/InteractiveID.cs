using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveID : MonoBehaviour
{
    [SerializeField] int interactiveCount;

    public void TouchObject()
    {
        InteractiveManager.Instance.PanelOpen(interactiveCount);
    }
}
