using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveManager : MonoSingleton<InteractiveManager>
{
    [SerializeField] List<GameObject> panels = new List<GameObject>();

    public void PanelOpen(int panelCount)
    {
         panels[panelCount].SetActive(true);
    }
}
