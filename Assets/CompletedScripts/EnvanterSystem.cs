using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnvanterSystem : MonoBehaviour
{
    [SerializeField] GameObject openPos, closePos;
    [SerializeField] GameObject envanterUIObject;
    [SerializeField] Button envanterOpenCloseButton;

    bool isOpen = false;


    void Start()
    {
        EnvanterClose();
        envanterOpenCloseButton.onClick.AddListener(EnvanterButton);
    }

    public void EnvanterOpen()
    {
        bool tempBool = true;
        isOpen = true;

        MoveMechanics.Instance.MoveStabile(envanterUIObject, openPos.transform.position, 1, ref tempBool);
    }

    public void EnvanterClose()
    {
        bool tempBool = true;
        isOpen = false;

        MoveMechanics.Instance.MoveStabile(envanterUIObject, closePos.transform.position, 1, ref tempBool);
    }

    private void EnvanterButton()
    {
        if (isOpen) EnvanterClose();
        else EnvanterOpen();
    }

}
