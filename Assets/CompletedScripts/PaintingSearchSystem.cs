using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingSearchSystem : MonoSingleton<PaintingSearchSystem>
{
    [SerializeField] GameObject _camera;
    [SerializeField] RectTransform targetObject;
    [SerializeField] float scaleDistance = 1f;
    [SerializeField] float minScale = 0.5f;
    [SerializeField] float maxScale = 1;

    public float GetScaleDistance()
    {
        return scaleDistance;
    }
    public RectTransform GetTargetObject()
    {
        return targetObject;
    }
    public float GetMinScale()
    {
        return minScale;
    }
    public float GetMaxScale()
    {
        return maxScale;
    }
    public void targetObjectOn()
    {
        targetObject.gameObject.SetActive(true);
    }
    public void targetObjectOff()
    {
        targetObject.gameObject.SetActive(false);
    }
    public void SetCamera(GameObject target)
    {
        FollowMouseAndScale followMouseAndScale = target.GetComponent<FollowMouseAndScale>();
        bool tempBool = true;

        CameraTargetFollow.Instance.enabled = false;
        CameraController.Instance.enabled = true;
        CharacterMove.Instance.gameObject.SetActive(false);
        CameraController.Instance.SetWorldSpaceCanvas(followMouseAndScale.GetCanvas());

        if (PaintingManager.Instance.CheckPanitngs(target))
        {
            PaintingCompetedSystem.Instance.ButtonActive();
        }
        else
        {
            CameraController.Instance.enabled = true;
            WorldSpaceButtonClick.Instance.enabled = true;

            followMouseAndScale.enabled = true;
        }

        MoveMechanics.Instance.MoveLerpQuaternion(_camera, followMouseAndScale.GetCameraPos().transform.rotation, 1, ref tempBool);
        MoveMechanics.Instance.MoveLerp(_camera, followMouseAndScale.GetCameraPos().transform.position, 1, ref tempBool);

    }

    public void CameraOff(GameObject target)
    {
        FollowMouseAndScale followMouseAndScale = target.GetComponent<FollowMouseAndScale>();

        CameraTargetFollow.Instance.enabled = true;
        CameraController.Instance.enabled = false;
        WorldSpaceButtonClick.Instance.enabled = false;
        CharacterMove.Instance.gameObject.SetActive(true);
        CameraController.Instance.SetWorldSpaceCanvas(followMouseAndScale.GetCanvas());
        PaintingManager.Instance.PaintingAdd(target);

        followMouseAndScale.enabled = false;
    }
}
