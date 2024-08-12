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
        PaintingSearchSystem.Instance.GetTargetObject().parent.parent.gameObject.GetComponent<FollowMouseAndScale>().enabled = false;
        exitButton.gameObject.SetActive(false);
        CameraTargetFollow.Instance.enabled = true;
        CameraController.Instance.enabled = false;
        CharacterMove.Instance.gameObject.SetActive(true);
        ObjectTooltip.Instance.enabled = true;
        WorldSpaceButtonClick.Instance.enabled = false;

        InteractiveManager.Instance.PerspectiveCameraOn();
    }


}
