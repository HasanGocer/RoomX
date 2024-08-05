using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaintingCompetedSystem : MonoSingleton<PaintingCompetedSystem>
{
    [SerializeField] Button exitButton;

    private void Start()
    {
        exitButton.onClick.AddListener(ExitButtonFunc);
    }

    public void ButtonActive()
    {
        exitButton.gameObject.SetActive(true);
    }

    private void ExitButtonFunc()
    {
        exitButton.gameObject.SetActive(false);
        CameraController.Instance.enabled = false;
        CharacterMove.Instance.gameObject.SetActive(true);

        InteractiveManager.Instance.PerspectiveCameraOn();
    }


}
